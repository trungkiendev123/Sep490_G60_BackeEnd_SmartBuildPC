﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sep490_G60_Backend_SmartBuildPC.Repositories;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using System.Net;

namespace Sep490_G60_Backend_SmartBuildPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildPCController : ControllerBase
    {

        private readonly IBuildPCRepository _repository;

        public BuildPCController(IBuildPCRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetList() {
            IEnumerable<CategoryDTO> list = await _repository.listCategory();
            var _response = new ApiResponse();
            try
            {
               
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = list;
                _response.IsSuccess = true;
                _response.Message = "List successfully";
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = "List fail";
                return StatusCode(StatusCodes.Status500InternalServerError,_response);
            }
        }
    }
}
