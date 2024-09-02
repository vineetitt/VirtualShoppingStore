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
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryRepository"></param>
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        /// <summary>
        /// GetAllCategory
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetAllCategory() { 

            var data= categoryRepository.GetAllCategory();

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



        /// <summary>
        /// GetCategoryById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]

        [Route("{id}")]


        public IActionResult GetCategoryById(int id) {
            var data =  categoryRepository.GetCategoryById(id);
            if (data == null)
            {
                return NotFound();
            }

            ResponseCategoryDto responseCategoryDto = new ResponseCategoryDto()
            {
                CategoryId = data.CategoryId,
                CategoryName = data.CategoryName,
            };

           return Ok(responseCategoryDto);
        }

        /// <summary>
        /// DeleteCategory
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
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// AddCategory
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("{id}")]

        public IActionResult AddCategory(string categoryname)
        {
            try
            {
                categoryRepository.AddCategory(categoryname);
                return Ok();    
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="updateCategoryDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPatch]
        [Route("{categoryId}")]

        public IActionResult UpdateCategoryByCategoryId(int categoryId, UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                categoryRepository.UpdateCategoryByCategoryId(categoryId, updateCategoryDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
