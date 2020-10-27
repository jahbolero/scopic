using System;
using System.ComponentModel.DataAnnotations;

namespace scopic_test_server.DTO
{
    public class ProductCreateDto
    {

        public Guid ProductId { get; set; }
        [MaxLength(30)]
        public string ProductName { get; set; }
        [MaxLength(200)]
        public string ProductDescription { get; set; }
        public string ImgUrl { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime UploadDate { get; set; }
    }
}