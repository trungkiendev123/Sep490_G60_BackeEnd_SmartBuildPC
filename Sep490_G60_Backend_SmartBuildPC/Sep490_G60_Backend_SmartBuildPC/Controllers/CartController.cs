using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sep490_G60_Backend_SmartBuildPC.Repositories;
using Sep490_G60_Backend_SmartBuildPC.Requests;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using Sep490_G60_Backend_SmartBuildPC.Service;
using Sep490_G60_Backend_SmartBuildPC.Validation;
using System.Data;
using System.Net;

namespace Sep490_G60_Backend_SmartBuildPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ValidateCart _validate;

        private readonly ICartRepository _repository;


        public CartController( ValidateCart validate, ICartRepository repository)
        {
            _validate = validate;
            _repository = repository;
        }
        [HttpPost("AddCart")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<ActionResult<ApiResponse>> AddCart(ChangeCartRequest request)
        {
            var email = User.Identity.Name;
            var _response = new ApiResponse();
            List<string> errors = _validate.validateCart(request);
            try
            {
                if (errors.Count > 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Message = "Add cart fail";
                    _response.ErrorMessages = errors;
                }
                else
                {
                    _repository.AddCart(email,request);
                    _response.StatusCode = HttpStatusCode.Created;
                    _response.IsSuccess = true;
                    _response.Message = "Add cart success";
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = "Add cart fail",
                    ErrorMessages = new List<string> { "Something error from the server" }
                };
            }

        }
        [HttpPut("UpdateCart")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<ActionResult<ApiResponse>> UpdateCart(ChangeCartRequest request)
        {
            var email = User.Identity.Name;
            var _response = new ApiResponse();
            List<string> errors = _validate.validateCart(request);
            try
            {
                if (errors.Count > 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Message = "Update cart fail";
                    _response.ErrorMessages = errors;
                }
                else
                {
                    _repository.UpdateCart(email, request);
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Message = "Update cart success";
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = "Update cart fail",
                    ErrorMessages = new List<string> { "Something error from the server" }
                };
            }

        }
        [HttpDelete("DeleteCart")]
        [Authorize(Roles = "CUSTOMER")]
        public async Task<ActionResult<ApiResponse>> DeleteCart(int productID)
        {
            var email = User.Identity.Name;
            var _response = new ApiResponse();
            bool check = _validate.isExistProductID(productID);
            List<string> errors = new();
            try
            {
                if (!check)
                {
                    errors.Add("ProductID not found");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Message = "Delete cart item fail";
                    _response.ErrorMessages = errors;
                }
                else
                {
                    _repository.DeleteCart(email, productID);
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Message = "Delete cart item success";
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = "Delete cart item fail",
                    ErrorMessages = new List<string> { "Something error from the server" }
                };
            }

        }
    }
}
