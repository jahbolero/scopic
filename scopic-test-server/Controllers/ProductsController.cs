
using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scopic_test_server.DTO;
using scopic_test_server.Interface;
using System.Linq;
using static scopic_test_server.Helper.Codes;
using Microsoft.AspNetCore.Authorization;

namespace scopic_test_server
{
    [Authorize]
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
        public ActionResult<List<ProductReadDto>> GetAllProducts([FromQuery] int Page, [FromQuery] string Sort, [FromQuery] string SearchString)
        {
            var result = _repository.GetAllProducts(Page, Sort, SearchString);
            return Ok(_mapper.Map<List<ProductReadDto>>(result.ToList()));
        }
        //GET api/products/
        [HttpGet]
        [Route("{ProductId}")]
        public ActionResult<ProductReadDto> GetProduct(Guid ProductId)
        {
            var result = _repository.GetProduct(ProductId);
            result.Bids = result.Bids.OrderByDescending(x => x.BidAmount);
            return Ok(_mapper.Map<ProductReadDto>(result));
        }
        //POST api/products/addProduct
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("addProduct")]
        public ActionResult AddProduct([FromForm] ProductCreateDto Product)
        {
            Product.ExpiryDate = Product.ExpiryDate.ToUniversalTime();
            var result = _repository.AddProduct(Product);
            if (result != ProductCode.Success)
                return BadRequest(result.GetDescription());
            return Ok(new { message = result.GetDescription() });
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("{productId}")]
        public ActionResult DeleteProduct(Guid ProductId)
        {
            var result = _repository.DeleteProduct(ProductId);
            if (result == false)
                return BadRequest("Something went wrong.");
            return Ok();
        }
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("editProduct")]
        public ActionResult EditProduct([FromForm] ProductUpdateDto Product)
        {
            Product.ExpiryDate = Product.ExpiryDate.ToUniversalTime();
            var result = _repository.EditProduct(Product);
            if (result != ProductCode.Success)
                return BadRequest(result.GetDescription());
            return Ok(new { message = result.GetDescription() });
        }
    }

}