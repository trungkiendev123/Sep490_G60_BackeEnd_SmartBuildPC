using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Repositories;
using Sep490_G60_Backend_SmartBuildPC.Requests;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using Sep490_G60_Backend_SmartBuildPC.Validation;
using System.Net;

namespace Sep490_G60_Backend_SmartBuildPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly SMARTPCContext _context;

        public AccountController(IAccountRepository repository, SMARTPCContext context)
        {
            _repository = repository;
            _context = context;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register(RegisterFormRequest request)
        {
            var _response = new ApiResponse();
            List<string> errors = new ValidateAccount(_context).validateAccount(request);
            try
            {
                if(errors.Count > 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Message = "Register Fail";
                    _response.ErrorMessages = errors;
                }
                else
                {
                    _repository.register(request);
                    _response.StatusCode = HttpStatusCode.Created;
                    _response.IsSuccess = true;
                    _response.Message = "Register Success";
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Message = "Register Fail";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
