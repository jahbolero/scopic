using System;
using System.Collections.Generic;
using scopic_test_server.Interface;
using System.Linq;
using scopic_test_server.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using scopic_test_server.Helper;
using System.IO;
using static scopic_test_server.Helper.Codes;
using Microsoft.AspNetCore.Http;
using MimeKit;
using scopic_test_server.Services;

namespace scopic_test_server.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ScopicContext _context;
        private readonly IMapper _mapper;
        private readonly S3UploadImage _S3UploadeImage;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public ProductRepository(ScopicContext context, IMapper mapper, AppSettings appSettings, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings;
            _emailService = emailService;
            _S3UploadeImage = new S3UploadImage(appSettings);
        }
        public IEnumerable<Product> GetAllProducts(int Page, string Sort, string SearchString)//Sort asc = true, desc = false
        {
            int itemCount = 10;
            IEnumerable<Product> productList = default;
            productList = SearchString == null ?
                         _context.Product.Where(x => x.Status != 1).Include(y => y.Bids) :
                         _context.Product.Where(x => (x.Status != 1) && (x.ProductName.Contains(SearchString) || x.ProductDescription.Contains(SearchString))).Include(y => y.Bids);

            if (Sort != null)
            {
                productList = Sort == "ASC" ?
                //Promote bidamount to nullable in order to handle Max throwing error with non nullables.
                productList = productList.OrderBy(x => x.Bids.Max(y => (decimal?)y.BidAmount)) :
                productList = productList.OrderByDescending(x => x.Bids.Max(y => (decimal?)y.BidAmount));
            }

            productList = productList.Skip((Page - 1) * itemCount).Take(itemCount);
            return productList;
        }

        public Product GetProduct(Guid ProductId)
        {
            var product = _context.Product.Include(x => x.Bids).Include("Bids.User").FirstOrDefault(y => y.ProductId == ProductId);
            return product;
        }
        public ProductCode AddProduct(ProductCreateDto Product)
        {
            if (DateTime.UtcNow > Product.ExpiryDate)
                return ProductCode.InvalidDate;
            Product.UploadDate = DateTime.UtcNow;
            var product = _mapper.Map<Product>(Product);
            product.ProductId = Guid.NewGuid();
            var imgName = GetImgName(Product.ImgFile, product.ProductId);
            product.ImgUrl = GetImgUrl(imgName);
            _S3UploadeImage.UploadFileAsync(Product.ImgFile, imgName).Wait();
            _context.Product.Add(product);
            _context.SaveChanges();
            return ProductCode.Success;
        }

        private static string GetImgUrl(string imgName)
        {
            return $"https://scopic-bucket.s3-ap-southeast-1.amazonaws.com/{imgName}";
        }

        private static string GetImgName(IFormFile ImgFile, Guid ProductId)
        {
            return $"{ProductId}{Path.GetExtension(ImgFile.FileName)}";
        }

        public bool DeleteProduct(Guid ProductId)
        {
            var product = _context.Product.FirstOrDefault(x => x.ProductId == ProductId);
            if (product == null)
                return false;
            var bids = _context.Bid.Where(x => x.ProductId == product.ProductId);
            _context.Bid.RemoveRange(bids);
            _context.Product.Remove(product);
            _context.SaveChanges();
            return true;
        }

        public ProductCode EditProduct(ProductUpdateDto Product)
        {
            var product = _context.Product.FirstOrDefault(x => x.ProductId == Product.ProductId);
            if (product == null)
                return ProductCode.Null;
            if (DateTime.UtcNow > Product.ExpiryDate)
                return ProductCode.InvalidDate;
            if (Product.ImgFile != null)
            {
                var imgName = GetImgName(Product.ImgFile, product.ProductId);
                product.ImgUrl = GetImgUrl(imgName);
                _S3UploadeImage.UploadFileAsync(Product.ImgFile, imgName).Wait();
            }

            product.ProductName = Product.ProductName;
            product.ProductDescription = Product.ProductDescription;
            product.ExpiryDate = Product.ExpiryDate;
            _context.SaveChanges();
            return ProductCode.Success;
        }

        public IEnumerable<Product> GetExpiredProducts()
        {
            var expiredProducts = _context.Product.Where(x => x.ExpiryDate <= DateTime.UtcNow && x.Status != 1 && x.Bids.Count() > 0).Include("Bids.User");
            return expiredProducts;
        }

        public void ProcessProducts()
        {
            var expiredProducts = GetExpiredProducts();
            var userProductList = new List<UserProduct>();
            var mailList = new List<MimeMessage>();
            foreach (var product in expiredProducts)
            {
                var highestBid = product.Bids.OrderByDescending(x => x.BidAmount).FirstOrDefault();
                var bidders = product.Bids.Select(x => x.User).Distinct();
                var userProduct = new UserProduct()
                {
                    UserProductId = Guid.NewGuid(),
                    ProductId = product.ProductId,
                    UserId = highestBid.UserId
                };
                product.Status = 1;
                userProductList.Add(userProduct);
                foreach (var bidder in bidders)
                {
                    var message = bidder.UserId == userProduct.UserId ? $"<h1>Congratulations, you won the bid for {product.ProductName}!</h1><p>Payment amount:${highestBid.BidAmount}</p><p>Bid date:{highestBid.BidDate}</p>" :
                     $"<h1>Unfortunately, you didn't win the bid for {product.ProductName}!</h1><p>Final highest bid amount is:${highestBid.BidAmount}</p>";
                    var mail = _emailService.NewMail(bidder.Username, $"Product Update {product.ProductName}", message);
                    mailList.Add(mail);
                }
            }
            _context.UserProducts.AddRange(userProductList);
            _emailService.SendEmails(mailList);
            _context.SaveChanges();
        }
    }
}