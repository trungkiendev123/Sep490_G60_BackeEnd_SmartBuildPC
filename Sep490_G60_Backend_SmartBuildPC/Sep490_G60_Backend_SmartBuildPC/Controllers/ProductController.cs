
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sep490_G60_Backend_SmartBuildPC.DTOs;
using Sep490_G60_Backend_SmartBuildPC.Repositories;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using System.Net;

namespace Sep490_G60_Backend_SmartBuildPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;
        public ProductController(IProductRepository _repository)
        {
            repository = _repository;
        }




        [HttpGet("GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> ListProducts(int id)
        {
            var _response = new ApiResponse();
            try
            {
                ProductDTO products = await repository.GetProduct(id);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = products;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }



        [HttpGet("GetProductsByBrand")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> GetProductsByBrand(string brandName)
        {
            var _response = new ApiResponse();
            try
            {
                List<ProductDTO> products = await repository.GetProductByBrand(brandName);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = products;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> GetAllProducts()
        {
            var _response = new ApiResponse();
            try
            {
                List<ProductDTO> products = await repository.GetAllProducts();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = products;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }





        [HttpGet("GetProductsByGroup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> GetProductsByGroup(string name)
        {
            var _response = new ApiResponse();
            try
            {
                List<ProductDTO> products = await repository.GetProductByGroup(name);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = products;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }
        [HttpGet("GetProductsByCategory")]
        public async Task<ActionResult<ApiResponse>> GetProductsByCategory(int cateID)
        {
            var _response = new ApiResponse();
            try
            {
                IEnumerable<ProductDTO> products = await repository.GetProductsByCategory(cateID);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = products;
                _response.IsSuccess = true;
                _response.Message = "Get data successfully";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.Message = "Get data fail";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
        [HttpGet("PreviewProduct")]
        public async Task<ActionResult<ApiResponse>> PreviewProduct(int productID)
        {
            var _response = new ApiResponse();
            try
            {
                PreviewProductDTO preview = await repository.PreviewProduct(productID);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = preview;
                _response.IsSuccess = true;
                _response.Message = "Get data successfully";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.Message = "Get data fail";
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}

