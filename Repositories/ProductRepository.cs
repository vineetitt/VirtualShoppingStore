using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.ProductDto;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// class ProductRepository
    /// </summary>

    public class ProductRepository:IProductRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext"></param>
        
        public ProductRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }

        /// <summary>
        /// GetAllProduct
        /// </summary>
        /// <returns></returns>
        
        public List<Product> GetAllProduct()
        {
            var data= virtualShoppingStoreDbContext.Products.ToList();
            if (data.Any())
            {
                return data;
            }

            throw new CustomException("No products found.", 200);
        }

        /// <summary>
        /// AddNewProduct
        /// </summary>
        /// <param name="addProductDto"></param>
        /// <returns></returns>

        public void AddNewProduct(AddProductDto addProductDto)
        {
            var x = virtualShoppingStoreDbContext.Categories.FirstOrDefault(y => y.CategoryId == addProductDto.CategoryId);

            if (x == null)
            {
                throw new CustomException("Category not found", 400);
            }

            else
            {

                var p = new Product();
                if (addProductDto.ProductName == p.ProductName)
                {
                    throw new CustomException("Product with this name already exists try another name", 400);
                }

                var product = new Product()
                {
                    ProductName = addProductDto.ProductName,
                    Description = addProductDto.Description,
                    Price = addProductDto.Price,
                    StockQuantity = addProductDto.StockQuantity,
                    //CategoryID = addProductDto.CategoryId,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    IsDeleted = false
                };

                virtualShoppingStoreDbContext.Products.Add(product);
                virtualShoppingStoreDbContext.SaveChanges();

            }

        }

        /// <summary>
        /// Delete Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public void DeleteProductById(int id)
        {
            var isdeleted = virtualShoppingStoreDbContext.Products.FirstOrDefault(X => X.ProductId == id);

            if (isdeleted == null)
            {
                throw new CustomException("No product available by this id",400);
            }

            virtualShoppingStoreDbContext.Products.Remove(isdeleted);
            virtualShoppingStoreDbContext.SaveChanges();
            
        }

        /// <summary>
        /// UpdateProductByid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchProductDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        
        public Product UpdateProductByid(int id, PatchProductDto patchProductDto)
        {
            
            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(y => y.ProductId == id);

            if (product == null) {
                throw new CustomException("No product found with this product id", 400);
            }

            product.ProductName = patchProductDto.ProductName ?? product.ProductName;
            product.Description = patchProductDto.Description ?? product.Description;
            product.Price = patchProductDto.Price ?? product.Price;
            product.StockQuantity = patchProductDto.StockQuantity ?? product.StockQuantity;
            product.CategoryId = patchProductDto.CategoryId ?? product.CategoryId;
            product.IsDeleted = patchProductDto.IsDeleted ?? product.IsDeleted;

            virtualShoppingStoreDbContext.SaveChanges();

            return product;

        }

        /// <summary>
        /// GetProductById
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Product GetProductById(int productId)
        {
            
            var productidcheck= virtualShoppingStoreDbContext.Products.FirstOrDefault(x=>x.ProductId==productId);
            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(x => x.ProductId == productId);

            if (product != null)
            {
                return product;
            }

            else
            {
                throw new CustomException("Product not found.",200);
            }

        }

    }

}
