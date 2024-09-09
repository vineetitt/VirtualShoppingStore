using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CategoryDto;
using VirtualShoppingStore.Models.DTO.ProductDto;
using VirtualShoppingStore.Models.DTO.UserDto;
using VirtualShoppingStore.Repositories;


namespace VirtualShoppingStore.Controllers
{
    /// <summary>
    /// Category Controller
    /// </summary>
    
    [Route("api/[controller]")]

    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        /// <summary>
        /// CategoryController class
        /// </summary>
        /// <param name="categoryRepository"></param>

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        /// <summary>
        /// GetAllCategories
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]

        public IActionResult GetAllCategories()
        {
            try
            {
                var data = categoryRepository.GetAllCategories();

                var responseCategoryDto = new List<ResponseCategoryDto>();

                foreach (var item in data)
                {

                    responseCategoryDto.Add(new ResponseCategoryDto()
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                    });

                }

                return Ok(responseCategoryDto);
            }
            
            catch(CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex) 
            {
                return StatusCode(500,$"An unexpected error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
 
        [HttpGet]

        [Route("{id}")]

        public IActionResult GetCategoryById(int id) {
            try
            {
                var data = categoryRepository.GetCategoryById(id);
                ResponseCategoryDto responseCategoryDto = new ResponseCategoryDto()
                {
                    CategoryId = data.CategoryId,
                    CategoryName = data.CategoryName,
                };
                return Ok(responseCategoryDto);
            }

            catch(CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <returns></returns>
        
        [HttpDelete]

        [Route("{id}")]

        public IActionResult DeleteCategoryById(int id)
        {
            try
            {
                categoryRepository.DeleteCategoryById(id);
                return Ok();
            }

            catch(CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// AddCategory
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns></returns>
        
        [HttpPost]
        
        public IActionResult AddCategory(string categoryname)
        {

            try
            {
                categoryRepository.AddCategory(categoryname);
                return Ok();    
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex) {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// UpdateCategory by categoryId
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="updateCategoryDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        [HttpPatch]

        [Route("{categoryId}")]

        public IActionResult UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto)
        {

            try
            {
                categoryRepository.UpdateCategory(categoryId, updateCategoryDto);
                return Ok();
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }

        }

    }

}
