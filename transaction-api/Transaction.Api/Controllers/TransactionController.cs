using MediatR;
using Microsoft.AspNetCore.Mvc;
using Transaction.Application.Commands;
using Transaction.Application.DTOs;
using Transaction.Application.Query;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Transaction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SaveTransaction")]
        public async Task<IActionResult> SaveTransaction([FromBody] SaveTransactionRequest request)
        {
            var command = new SaveTransactionCommand(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("GetByIdAndDate")]
        public async Task<IActionResult> GetByIdAndDate([FromBody] GetByIdAndDateRequest request)
        {
            var command = new GetTransactionByIdAndDateQuery(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
