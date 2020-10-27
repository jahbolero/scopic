using System;
using System.Collections.Generic;
using scopic_test_server.Data;

namespace scopic_test_server.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts(int Page, int Role);
        Product GetProduct(Guid ProductId);

    }
}