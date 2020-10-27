using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace scopic_test_server.DTO
{
    public class ProductCreateDto
    {
        [MaxLength(30)]
        public string ProductName { get; set; }
        [MaxLength(200)]
        public string ProductDescription { get; set; }
        public string ImgUrl { get; set; }
        public IFormFile ImgFile { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime UploadDate { get; set; }
    }
}