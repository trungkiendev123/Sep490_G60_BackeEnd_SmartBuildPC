
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
 public async Task<ActionResult<ApiResponse>> GetProducts(int id)
 {
     var _response = new ApiResponse();
     try
     {
         var product = await repository.GetProduct(id);
         if (product == null)
         {
             _response.IsSuccess = false;
             _response.ErrorMessages = new List<string> { "Product not found." };
             return NotFound(_response);
         }
         _response.StatusCode = HttpStatusCode.OK;
         _response.Result = product;
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
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }


[HttpGet("GetAllProducts")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public async Task<ActionResult<ApiResponse>> GetAllProducts(int pageNumber = 1, int pageSize = 50)
{
    var _response = new ApiResponse();
    try
    {
        var products = await repository.GetAllProducts(pageNumber, pageSize);
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = products;
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
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }



        [HttpGet("SearchProducts")]
        public async Task<ActionResult<ApiResponse>> GetProductsByKeyword(string keyword, int pageNumber = 1, int pageSize = 50)
        {
            var _response = new ApiResponse();
            try
            {
                List<ProductDTO> products = await repository.GetProductsByKeyword(keyword, pageNumber, pageSize);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = products;
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



        [HttpGet("GetProductDetailsWithSimilarPriceRange")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public async Task<ActionResult<ApiResponse>> GetProductDetailsWithSimilarPriceRange(int id, decimal priceRange)
{
    var _response = new ApiResponse();
    try
    {
        var productDetails = await repository.GetProductDetailsWithSimilarPriceRange(id, priceRange);
        if (productDetails == null)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { "Product not found." };
            return NotFound(_response);
        }
        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = productDetails;
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



     [HttpPost("CreateProduct")]
public async Task<ActionResult<ApiResponse>> CreateProduct([FromBody] CreateProductDTO createProductDTO)
{
    var response = new ApiResponse();
    try
    {
        var product = await repository.CreateProduct(createProductDTO);
        response.StatusCode = HttpStatusCode.Created;
        response.Result = product;
        response.IsSuccess = true;
        return CreatedAtAction(nameof(GetProducts), new { id = product.ProductId }, response);
    }
    catch (Exception ex)
    {
        response.IsSuccess = false;
        response.ErrorMessages = new List<string> { ex.Message };
        return StatusCode(StatusCodes.Status500InternalServerError, response);
    }
}



[HttpDelete("DeleteProduct/{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<ActionResult<ApiResponse>> DeleteProduct(int id)
{
    var _response = new ApiResponse();
    try
    {
        var isDeleted = await repository.DeleteProduct(id);
        if (!isDeleted)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { "Product not found." };
            _response.StatusCode = HttpStatusCode.NotFound;
            return NotFound(_response);
        }

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


[HttpPut("UpdateProduct/{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<ActionResult<ApiResponse>> UpdateProduct(int id, [FromBody] UpdateProductDTO updateProductDTO)
{
    var _response = new ApiResponse();
    try
    {
        var updatedProduct = await repository.UpdateProduct(id, updateProductDTO);
        if (updatedProduct == null)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string> { "Product not found." };
            _response.StatusCode = HttpStatusCode.NotFound;
            return NotFound(_response);
        }

        _response.StatusCode = HttpStatusCode.OK;
        _response.Result = updatedProduct;
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
