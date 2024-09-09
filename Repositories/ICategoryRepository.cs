using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CategoryDto;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    
    public interface ICategoryRepository
    {

        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        
        public List<Category> GetAllCategories();

        /// <summary>
        /// GetCategoryById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Category GetCategoryById(int id);

        /// <summary>
        /// DeleteCategoryById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public void DeleteCategoryById(int id);

        /// <summary>
        /// AddCategory
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>

        public void AddCategory(string categoryName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="updateCategoryDto"></param>
        
        public void UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto);

    }

}
