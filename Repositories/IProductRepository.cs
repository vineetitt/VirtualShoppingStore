using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.ProductDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// IProductRepository
    /// </summary>
    
    public interface IProductRepository
    {

        /// <summary>
        /// GetAllProduct
        /// </summary>
        /// <returns></returns>
        List<Product> GetAllProduct(string? filteron, string? queryname, int pagenumber, int pagesize);

        /// <summary>
        /// AddNewProduct
        /// </summary>
        /// <returns></returns>
        void AddNewProduct(AddProductDto addProductDto);

        /// <summary>
        /// Delete Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    
        void DeleteProductById(int id);

        /// <summary>
        /// UpdateProductByid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchProductDto"></param>
        /// <returns></returns>
        
        public Product UpdateProductByid(int id, PatchProductDto patchProductDto );

        /// <summary>
        /// GetProductById
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        
        public Product GetProductById(int productId);

    }

}
