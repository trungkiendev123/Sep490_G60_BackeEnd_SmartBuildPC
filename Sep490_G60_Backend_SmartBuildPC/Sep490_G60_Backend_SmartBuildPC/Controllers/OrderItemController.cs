using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sep490_G60_Backend_SmartBuildPC.Repositories;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using System.Net;

namespace Sep490_G60_Backend_SmartBuildPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {


        private readonly IOrderItemRepository repository;
        public OrderItemController(IOrderItemRepository _repository)
        {
            repository = _repository;
        }


        [HttpGet("GetOrderItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetOrderItems(int orderId)
        {
            var response = new ApiResponse();
            try
            {
                var orderItems = await repository.GetOrderItems(orderId);

                if (orderItems == null || orderItems.Count == 0)
                {
                    response.IsSuccess = false;
                    response.ErrorMessages = new List<string> { "Order items not found." };
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = orderItems;
                response.IsSuccess = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }




        [HttpPost("CreateOrderItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> CreateOrderItem([FromBody] CreateOrderItemDTO createOrderItemDTO)
{
        var _response = new ApiResponse();
        try
    {
        await repository.CreateOrderItemAsync(createOrderItemDTO);
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



        [HttpGet("GetAllOrderItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<OrderItemDTO>>> GetAllOrderItems(int page = 1, int pageSize = 10)
        {
            try
            {
                var orderItems = await repository.GetAllOrderItems(page, pageSize);
                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving all order items: {ex.Message}");
            }
        }
    }
}
