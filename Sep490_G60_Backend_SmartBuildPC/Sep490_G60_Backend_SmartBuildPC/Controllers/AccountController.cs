using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<ApiResponse>> Login(LoginFormRequest request)
        {
            try
            {
                if (_repository.GetAccountByEmail(request.email) == null)
                {
                    return new ApiResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        Message = "Wrong email",
                        ErrorMessages = new List<string> { "Email not exist in the system" }
                    };
                }
                Account account = await _repository.GetAccount(request.email, request.password);
                if (account == null)
                {
                    return new ApiResponse
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        IsSuccess = false,
                        Message = "Wrong password",
                        ErrorMessages = new List<string> { "Password for this email is wrong" }
                    };

                }
                else
                {
                    if (account.Status != 1)
                    {
                        return new ApiResponse
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            IsSuccess = false,
                            Message = "Your account has been banned",
                            ErrorMessages = new List<string> { "Your account has been banned" }
                        };
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
                    return new ApiResponse
                    {
                        StatusCode = HttpStatusCode.OK,
                        IsSuccess = true,
                        Message = "Login successfully",
                        TokenInformation = tokenInformation
                    };

                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = "Login fail",
                    ErrorMessages = new List<string> { "Something error from the server" }
                };

            }



        }

        [HttpPost("logout")]
        public async Task<ActionResult<ApiResponse>> Logout()
        {
            try
            {
                var email = User.Identity.Name;
                var user = await _repository.GetAccountByEmail(email);
                if (user == null) return BadRequest();
                user.RefreshToken = null;
                _repository.Update(user);
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Message = "Logout successfully"
                };
            }
            catch(Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = "Logout fail",
                    ErrorMessages = new List<string> { "Something error from the server" }
                };
            }
            
        }

        [HttpPost("AddAccount")]
        public async Task<ActionResult<ApiResponse>> AddAccount(AddAccountRequest request)
        {
            var _response = new ApiResponse();
            List<string> errors = _validate.validateAddAccount(request);
            try
            {
                if (errors.Count > 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Message = "Add account fail";
                    _response.ErrorMessages = errors;
                }
                else
                {
                    _repository.AddAccount(request);
                    _response.StatusCode = HttpStatusCode.Created;
                    _response.IsSuccess = true;
                    _response.Message = "Add account success";
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = "Add account fail",
                    ErrorMessages = new List<string> { "Something error from the server" }
                };
            }

        }
        [HttpPut("ChangeStatus")]
        public async Task<ActionResult<ApiResponse>> ChangeStatus(ChangeStatusRequest request)
        {
            var _response = new ApiResponse();
            List<string> errors = _validate.validateChangeStatus(request);
            try
            {
                if (errors.Count > 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.Message = "Change status account fail";
                    _response.ErrorMessages = errors;
                }
                else
                {
                    _repository.ChangeStatusAccount(request.status,request.AccountID);
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.IsSuccess = true;
                    _response.Message = "Change status account success";
                }
                return Ok(_response);
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    IsSuccess = false,
                    Message = "Change status account fail",
                    ErrorMessages = new List<string> { "Something error from the server" }
                };
            }

        }
    }
}
