using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sep490_G60_Backend_SmartBuildPC.Repositories;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using System.Net;

namespace Sep490_G60_Backend_SmartBuildPC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet("GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> GetAllCategories()
        {
            var _response = new ApiResponse();
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = categories;
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



        [HttpGet("GetCategoryById/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetCategoryById(int categoryId)
        {
            var _response = new ApiResponse();
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = category;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
                return NotFound(_response);
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
