
using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scopic_test_server.DTO;
using scopic_test_server.Interface;
using System.Linq;

namespace scopic_test_server
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //GET api/products/
        [HttpGet]
        public ActionResult<List<ProductReadDto>> GetAllProducts(int Page, int Role, bool? Sort, string SearchString)
        {
            var result = _repository.GetAllProducts(Page, Role, Sort, SearchString);
            return Ok(_mapper.Map<List<ProductReadDto>>(result.ToList()));
        }
        //GET api/products/
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProductReadDto> GetProduct(Guid ProductId)
        {
            var result = _repository.GetProduct(ProductId);
            return Ok(_mapper.Map<ProductReadDto>(result));
        }
        //POST api/products/addProduct
        [HttpPost]
        [Route("addProduct")]
        public ActionResult<ProductReadDto> AddProduct([FromForm] ProductCreateDto Product)
        {
            var result = _repository.AddProduct(Product);
            return Ok(_mapper.Map<ProductReadDto>(result));
        }
    }

}