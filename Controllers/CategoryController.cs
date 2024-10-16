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
    /// Handles HTTP requests related to categories in the VirtualShoppingStore application.
    /// </summary>

    [Route("api/[controller]")]

    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="categoryRepository">The repository used for managing categories.</param>

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Retrieves a list of all categories.
        /// </summary>
        /// <returns>A list of <see cref="ResponseCategoryDto"/> objects representing all categories.</returns>
        /// <response code="200">Returns the list of categories.</response>
        /// <response code="500">If an internal server error occurs.</response>

        [HttpGet]

        public IActionResult GetAllCategories()
        {
            try
            {
                var categoryData = categoryRepository.GetAllCategories();

                var responseCategoryDto = new List<ResponseCategoryDto>();

                foreach (var item in categoryData)
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
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Retrieves a specific category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A <see cref="ResponseCategoryDto"/> object representing the specified category.</returns>
        /// <response code="200">Returns the category.</response>
        /// <response code="404">If the category with the specified ID is not found.</response>
        /// <response code="500">If an internal server error occurs.</response>

        [HttpGet]

        [Route("{id}")]

        public IActionResult GetCategoryById(int id) {
            try
            {
                var categoryData = categoryRepository.GetCategoryById(id);

                ResponseCategoryDto responseCategoryDto = new ResponseCategoryDto()
                {
                    CategoryId = categoryData.CategoryId,
                    CategoryName = categoryData.CategoryName,
                };

                return Ok(responseCategoryDto);
            }

            catch(CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>HTTP response indicating the result of the delete operation.</returns>
        /// <response code="200">If the category was successfully deleted.</response>
        /// <response code="404">If the category with the specified ID is not found.</response>
        /// <response code="500">If an internal server error occurs.</response>

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
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="categoryName">The name of the category to add.</param>
        /// <returns>HTTP response indicating the result of the add operation.</returns>
        /// <response code="200">If the category was successfully added.</response>
        /// <response code="500">If an internal server error occurs.</response>

        [HttpPost]
        
        public IActionResult AddCategory(string categoryName)
        {

            try
            {
                categoryRepository.AddCategory(categoryName);
                return Ok();    
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

       /// <summary>
        /// Updates an existing category by its ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to update.</param>
        /// <param name="updateCategoryDto">The data to update the category with.</param>
        /// <returns>HTTP response indicating the result of the update operation.</returns>
        /// <response code="200">If the category was successfully updated.</response>
        /// <response code="400">If the update operation failed due to a bad request.</response>
        /// <response code="500">If an internal server error occurs.</response>
        /// <exception cref="Exception">Throws an exception if an error occurs.</exception>

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
                return BadRequest(ex.Message);
            }

        }

    }

}
