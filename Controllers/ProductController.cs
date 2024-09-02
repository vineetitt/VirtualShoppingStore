using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.ProductDto;
using VirtualShoppingStore.Repositories;

namespace VirtualShoppingStore.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// 

    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;


        /// <summary>
        /// 
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
        public IActionResult GetAllProduct()
        {
            var data = productRepository.GetAllProduct();
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
            catch (Exception ex) { 
                return BadRequest(ex.Message);
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
            var isdeleted = productRepository.DeleteProductById(id);

            if (isdeleted == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return NoContent();
            
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
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
            
        }



        /// <summary>
        /// 
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
            catch (Exception ex) { 
                throw new Exception(ex.Message);    
            }
            
        }

    }
}
