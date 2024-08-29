using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;

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
        String AddNewProduct(Product product);
    }
}
