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
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Category> GetAllCategory();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Category GetCategoryById(int id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void DeleteCategoryById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public void AddCategory(string categoryName);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="updateCategoryDto"></param>
        /// <returns></returns>
        public void UpdateCategoryByCategoryId(int categoryId, UpdateCategoryDto updateCategoryDto);

    }


}
