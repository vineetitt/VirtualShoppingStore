using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;

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

        public Product AddNewProduct(Product product)
        {
            virtualShoppingStoreDbContext.Products.Add(product);
            virtualShoppingStoreDbContext.SaveChanges();
            return product;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProduct()
        {
            var data= virtualShoppingStoreDbContext.Products.ToList();

            return data;
        }
    }
}
