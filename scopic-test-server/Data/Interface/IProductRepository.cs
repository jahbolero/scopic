using System;
using System.Collections.Generic;
using scopic_test_server.Data;
using scopic_test_server.DTO;
using static scopic_test_server.Helper.Codes;

namespace scopic_test_server.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts(int Page, bool? Sort, string SearchString);
        Product GetProduct(Guid ProductId);
        ProductCode AddProduct(ProductCreateDto Product);

    }
}