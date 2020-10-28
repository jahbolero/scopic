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
        public IEnumerable<Product> GetAllProducts(int Page, bool? Sort, string SearchString)//Sort asc = true, desc = false
        {
            IEnumerable<Product> productList = default;
            productList = SearchString == null ?
                         _context.Product.Where(x => x != null).Include(y => y.Bids) :
                         _context.Product.Where(x => x.ProductName.Contains(SearchString) || x.ProductDescription.Contains(SearchString)).Include(y => y.Bids);

            if (Sort != null)
            {
                productList = Sort == true ?
                //Promote bidamount to nullable in order to handle Max throwing error with non nullables.
                productList = productList.OrderBy(x => x.Bids.Max(y => (decimal?)y.BidAmount)) :
                productList = productList.OrderByDescending(x => x.Bids.Max(y => (decimal?)y.BidAmount));
            }
            productList = productList.Skip((Page - 1) * 15).Take(15);
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
            product.ImgUrl = $"{product.ProductId}{Path.GetExtension(Product.ImgFile.FileName)}";
            _S3UploadeImage.UploadFileAsync(Product.ImgFile, product.ImgUrl).Wait();
            _context.Product.Add(product);
            _context.SaveChanges();
            return ProductCode.Success;
        }
    }
}