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

namespace scopic_test_server.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ScopicContext _context;
        private readonly IMapper _mapper;
        private readonly S3UploadImage _S3UploadeImage;

        public ProductRepository(ScopicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _S3UploadeImage = new S3UploadImage();
        }
        public IEnumerable<Product> GetAllProducts(int Page, string Sort, string SearchString)//Sort asc = true, desc = false
        {
            int itemCount = 10;
            IEnumerable<Product> productList = default;
            productList = SearchString == null ?
                         _context.Product.Where(x => x != null).Include(y => y.Bids) :
                         _context.Product.Where(x => x.ProductName.Contains(SearchString) || x.ProductDescription.Contains(SearchString)).Include(y => y.Bids);

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
            var product = _context.Product.FirstOrDefault();
            if (product == null)
                return false;
            var bids = _context.Bid.Where(x => x.ProductId == product.ProductId);
            _context.Bid.RemoveRange(bids);
            _context.Product.RemoveRange(product);
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
    }
}