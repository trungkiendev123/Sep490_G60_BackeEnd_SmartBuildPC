using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Repositories;
using Sep490_G60_Backend_SmartBuildPC.Requests;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using Sep490_G60_Backend_SmartBuildPC.Service;
using Sep490_G60_Backend_SmartBuildPC.Validation;
using System.Net;

namespace Sep490_G60_Backend_SmartBuildPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly ValidateAccount _validate;
        private readonly TokenGenerate _token;

        public AccountController(IAccountRepository repository, ValidateAccount validate, TokenGenerate token)
        {
            _repository = repository;
            _validate = validate;
            _token = token;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse>> Register(RegisterFormRequest request)
        {
            var _response = new ApiResponse();
            List<string> errors = _validate.validateAccount(request);
            try
            {
                if (errors.Count > 0)
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
        public async Task<IActionResult> Login(LoginFormRequest request)
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
            Account account = await _repository.GetAccount(request.email, request.password);
            if (account == null)
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
                if (account.Status != 1)
                {
                    return Unauthorized(
                        new ApiResponse
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            IsSuccess = false,
                            Message = "Your account has been banned",
                            ErrorMessages = new List<string> { "Your account has been banned" }
                        }
                    );
                }
                var accessToken = await _token.generateToken(request);
                var refreshToken = await _token.GenerateRefreshToken();
                var tokenInformation = new TokenRefresh
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
                account.RefreshToken = refreshToken;
                account.Expired = DateTime.Now.AddDays(7);
                _repository.Update(account);
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
