using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sep490_G60_Backend_SmartBuildPC.Repositories;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using System.Net;

namespace Sep490_G60_Backend_SmartBuildPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {


        private readonly IOrderRepository _orderRepository;
        private readonly IEmailService _emailService;

        public OrderController(IOrderRepository orderRepository, IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _emailService = emailService;
        }


        


        [HttpPost("UpdateOrderStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateOrderStatus([FromBody] UpdateOrderStatusDTO UpdateOrderStatusDTO)
        {
            var _response = new ApiResponse();
            try
            {
                await _orderRepository.UpdateOrderStatusAsync(UpdateOrderStatusDTO);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
