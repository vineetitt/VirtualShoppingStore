﻿using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.ProductDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Product> GetAllProduct();


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        void AddNewProduct(AddProductDto addProductDto);


        /// <summary>
        /// Delete Product By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteProductById(int id);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchProductDto"></param>
        /// <returns></returns>
        public Product UpdateProductByid(int id, PatchProductDto patchProductDto );





        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetProductById(int productId);
    }
}
