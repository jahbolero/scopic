
using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scopic_test_server.DTO;
using scopic_test_server.Interface;

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
        public ActionResult<ProductReadDto> GetAllProducts(int Page, int Role)
        {
            var result = _repository.GetAllProducts(Page, Role);
            return Ok(_mapper.Map<ProductReadDto>(result));
        }
        //GET api/products/
        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProductReadDto> GetProduct(Guid ProductId)
        {
            var result = _repository.GetProduct(ProductId);
            return Ok(_mapper.Map<ProductReadDto>(result));
        }
    }

}