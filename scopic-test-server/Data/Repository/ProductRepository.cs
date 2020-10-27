using System;
using System.Collections.Generic;
using scopic_test_server.Interface;
using System.Linq;
using scopic_test_server.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace scopic_test_server.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ScopicContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(ScopicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

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
            var product = _context.Product.FirstOrDefault(x => x.ProductId == ProductId);
            return product;
        }
        public Product AddProduct(ProductCreateDto Product)
        {
            Product.UploadDate = DateTime.UtcNow;
            Product.ImgUrl = UploadImage("Image Location");
            var product = _mapper.Map<Product>(Product);
            product.ProductId = Guid.NewGuid();
            _context.Product.Add(product);
            _context.SaveChanges();
            return product;
        }

        public string UploadImage(string imageFile)
        {
            return imageFile;
        }
    }
}