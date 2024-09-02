using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.ProductDto;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    public class SQLProductRepository:IProductRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext"></param>
        public SQLProductRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="addProductDto"></param>
        /// <returns></returns>
        public void AddNewProduct(AddProductDto addProductDto )
        {
            var x = virtualShoppingStoreDbContext.Products.FirstOrDefault(y => y.CategoryId == addProductDto.CategoryId);
            if(x == null)
            {
                throw new Exception("category not found");
            }
            else
            {
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
        /// GetAllProduct
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProduct()
        {
            var data= virtualShoppingStoreDbContext.Products.ToList();

            return data;
        }

        




        /// <summary>
        /// Delete Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string IProductRepository.DeleteProductById(int id)
        {
            var isdeleted = virtualShoppingStoreDbContext.Products.FirstOrDefault(X => X.ProductId == id);

            if (isdeleted == null)
            {
                return "Not Found";
            }

            virtualShoppingStoreDbContext.Products.Remove(isdeleted);
            virtualShoppingStoreDbContext.SaveChanges();
            return "Deleted Successfully";
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchProductDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Product UpdateProductByid(int id, PatchProductDto patchProductDto)
        {
            
            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(y => y.ProductId == id);

            if (product == null) {
                throw new Exception("No product found with this product id");
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
        /// 
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
                throw new Exception("Product not found.");
            }

        }
    }
}
