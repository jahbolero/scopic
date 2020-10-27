using System;
using System.Collections.Generic;
using scopic_test_server.Interface;
using System.Linq;
using scopic_test_server.DTO;
using AutoMapper;

namespace scopic_test_server.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ScopicContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(ScopicContext context, IMapper _mapper)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAllProducts(int Page, int Role)
        {
            var productList = _context.Product.Where(x => x != null);
            return productList;
        }

        public Product GetProduct(Guid ProductId)
        {
            var product = _context.Product.FirstOrDefault(x => x.ProductId == ProductId);
            return product;
        }
        public Product AddProduct(ProductCreateDto Product)
        {
            Product.ProductId = Guid.NewGuid();
            Product.UploadDate = DateTime.UtcNow;
            Product.ImgUrl = UploadImage("Image Location");
            var product = _mapper.Map<Product>(Product);
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