using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scopic_test_server.DTO;
using scopic_test_server.Interface;
using static scopic_test_server.Helper.Codes;

namespace scopic_test_server
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IBidRepository _repository;
        private readonly IMapper _mapper;

        public BidsController(IBidRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("getBidsByProduct/{ProductId}")]
        public ActionResult<IEnumerable<BidReadDto>> GetBidsByProduct(Guid ProductId)
        {
            var result = _repository.GetBidsByProduct(ProductId);
            return Ok(_mapper.Map<IEnumerable<BidReadDto>>(result));
        }
        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("addBid")]
        public ActionResult AddBid(BidCreateDto Bid)
        {
            var userId = HttpContext.User.Identity.Name;
            Bid.UserId = Guid.Parse(userId);
            var result = _repository.AddBid(Bid);

            if (result != BidCode.Success)
                return BadRequest(result.GetDescription());
            return Ok(new { message = result.GetDescription() });
        }
    }

}