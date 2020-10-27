using System.Collections.Generic;
using scopic_test_server.Data;

namespace scopic_test_server.DTO
{
    public class ProductPaginateDto
    {
        public IEnumerable<Product> Product { get; set; }
        public int TotalCount { get; set; }
        public int ViewedCount { get; set; }

    }
}