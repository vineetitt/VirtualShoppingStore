using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.ProductDto;
using VirtualShoppingStore.Repositories;

namespace VirtualShoppingStore.Controllers
{
    /// <summary>
    /// ProductController class
    /// </summary>

    [Route("api/[controller]")]

    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        /// <summary>
        /// ProductController constructor
        /// </summary>
        /// <param name="productRepository"></param>
        
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <summary>
        /// Show all Products
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]

        public IActionResult GetAllProduct([FromQuery] string? filteron, [FromQuery] string? queryname, [FromQuery] int pagenumber=1, [FromQuery] int pagesize=50)
        {
            
            try
            {
                var data = productRepository.GetAllProduct(filteron, queryname, pagenumber, pagesize );
                var responseProductDto = new List<ResponseProductDto>();
                foreach (var item in data)
                {
                    responseProductDto.Add(new ResponseProductDto()
                    {
                        ProductName = item.ProductName,
                        Description = item.Description,
                        Price = item.Price,
                        StockQuantity = item.StockQuantity,
                        ProductId = item.ProductId,
                        CategoryId = item.CategoryId,
                        CreatedAt = item.CreatedAt,
                        IsDeleted = item.IsDeleted,
                    });

                }

                return Ok(responseProductDto);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="addProductDto"></param>
        /// <returns></returns>
        
        [HttpPost]

        public IActionResult AddNewProduct([FromBody] AddProductDto addProductDto)
        {
            try
            {
                productRepository.AddNewProduct(addProductDto);
                return Ok();
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpDelete]

        [Route("{id}")]

        public IActionResult DeleteProductById(int id)
        {
            try
            {
                productRepository.DeleteProductById(id);
                return Ok();
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex) 
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// Update Product By id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchProductDto"></param>
        /// <returns></returns>
        
        [HttpPatch]

        [Route("{id}")]

        public IActionResult UpdateProductByid(int id, PatchProductDto patchProductDto )
        {
           
            try
            {
                var response = productRepository.UpdateProductByid(id, patchProductDto);
                return Ok(response);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
        }

        /// <summary>
        /// GetProductById
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        [HttpGet]

        [Route("{productId}")]

        public IActionResult GetProductById(int productId)
        {
            try
            {
                var product = productRepository.GetProductById(productId);
                return Ok(product); 
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
        }

    }

}
