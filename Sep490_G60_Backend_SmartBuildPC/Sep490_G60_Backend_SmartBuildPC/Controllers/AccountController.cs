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

        [HttpPost("login")]
        public IActionResult Login(LoginFormRequest request)
        {
            if (_repository.GetAccountByEmail(request.email) == null)
            {
                return NotFound(
                        new ApiResponse
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            IsSuccess = false,
                            Message = "Wrong email",
                            ErrorMessages = new List<string> { "Email not exist in the system" }
                        }
                    );
            }
            if (_repository.GetAccount(request.email, request.password) == null)
            {
                return NotFound(
                        new ApiResponse
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            IsSuccess = false,
                            Message = "Wrong password",
                            ErrorMessages = new List<string> { "Password for this email is wrong" }
                        }
                    );
            }
            else
            {
                var user = _authenticationRepository.checkLoginEmployee(userRequest, false).Item2;
                var accessToken = _manageToken.generateToken(userRequest, false);
                var refreshToken = _manageToken.GenerateRefreshToken();
                var tokenInformation = new TokenModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpired = DateTime.Now.AddDays(7);
                _accountRepository.UpdateEmployee(user);
                if (user.Status == 0)
                {
                    _employeeRepository.ActivateEmployee(user.Id.ToString());
                }
                return Ok(
                    new ApiResponse
                    {
                        //StatusCode = HttpStatusCode.OK,
                        IsSuccess = true,
                        Message = "Login successfully",
                        TokenInformation = tokenInformation
                    }
                    ); ;
            }

        }
    }
}
