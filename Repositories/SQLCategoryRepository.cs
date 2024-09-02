using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CategoryDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SQLCategoryRepository: ICategoryRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;






        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext"></param>
        public SQLCategoryRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        public void AddCategory(string categoryName)
        {
            var convert = new Category()
            {
                CategoryName = categoryName,
            };
            
            virtualShoppingStoreDbContext.Categories.Add(convert);
            virtualShoppingStoreDbContext.SaveChanges();
            
               
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteCategoryById(int id)
        {
            var data= virtualShoppingStoreDbContext.Categories.FirstOrDefault(x=>x.CategoryId==id);
            
            //if (data != null) {
            //    return "deleted successfully";
            //}

            //return "Not Deleted";

            if (data == null) {
                throw new Exception("Invalid category Id");
            }


            var products = virtualShoppingStoreDbContext.Products.Any(p => p.CategoryId == id);
            if (products)
            {
                throw new Exception("Cannot delete category because it is being used by products.");
            }
            else
            {
                virtualShoppingStoreDbContext.Categories.Remove(data);
                virtualShoppingStoreDbContext.SaveChanges();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAllCategory()
        {
            var categorydata= virtualShoppingStoreDbContext.Categories.ToList();
            return categorydata;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetCategoryById(int id) { 
            var data= virtualShoppingStoreDbContext.Categories.FirstOrDefault(x=>x.CategoryId==id);

            if (data == null) {
               throw new Exception("No such category id found");
            }

            return data;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="updateCategoryDto"></param>
        public void UpdateCategoryByCategoryId(int categoryId, UpdateCategoryDto updateCategoryDto)
        {
            var category = virtualShoppingStoreDbContext.Categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (category == null)
            {
                throw new Exception("Category not found.");
            }

            
            category.CategoryName = updateCategoryDto.CategoryName ?? category.CategoryName;
            
            virtualShoppingStoreDbContext.Categories.Update(category);
            virtualShoppingStoreDbContext.SaveChanges();
        }

    }
}
