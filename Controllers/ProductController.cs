using Microsoft.AspNetCore.Authorization;
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

    //[Authorize]

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
        /// Retrieves all products with optional filtering and pagination.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
        /// <param name="pagesize">The number of products per page (default is 50).</param>
        /// <returns>A list of products.</returns>

        [HttpGet]

        public IActionResult GetAllProducts([FromQuery] int? categoryId, [FromQuery] int pageNumber=1, [FromQuery] int pagesize=50)
        {
            
            try
            {
                var data = productRepository.GetAllProducts(categoryId, pageNumber, pagesize );

                var totalProducts = productRepository.GetProductCount(categoryId);

                var maxPage = (int)Math.Ceiling((double)totalProducts / pagesize);

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

                return Ok(new { data =  responseProductDto, maxPage });
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

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="addProductDto">The product data to add.</param>
        /// <returns>An IActionResult indicating the outcome of the operation.</returns>

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
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Marks a product as deleted by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>An IActionResult indicating the outcome of the operation.</returns>
        
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
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Updates a product's details via PATCH.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="patchProductDto">The product data to update.</param>
        /// <returns>The updated product data.</returns>

        [HttpPatch]

        [Route("{id}")]

        public IActionResult UpdateProductById(int id, [FromBody]PatchProductDto patchProductDto )
        {
           
            try
            {
                var response = productRepository.UpdateProductById(id, patchProductDto);
                return Ok(response);
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

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve.</param>
        /// <returns>The product data.</returns>

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
                return BadRequest(ex.Message);
            }
            
        }

    }

}
