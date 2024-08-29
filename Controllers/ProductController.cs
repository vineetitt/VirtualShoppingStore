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
        public IActionResult AddNewProduct([FromBody]AddProductDto addProductDto)
        {
            Product product = new Product
            {
                ProductName= addProductDto.ProductName,
                Description= addProductDto.Description,
                Price = addProductDto.Price,
                StockQuantity = addProductDto.StockQuantity,
                CategoryId = addProductDto.CategoryId,
                IsDeleted= addProductDto.IsDeleted,
            };

            var data = productRepository.AddNewProduct(product);

            var addedproductdto = new AddProductDto()
            {
                CategoryId = product.CategoryId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                IsDeleted = product.IsDeleted,
            };
            return CreatedAtAction(nameof(GetAllProduct), addedproductdto);
            
        }
    }
}
