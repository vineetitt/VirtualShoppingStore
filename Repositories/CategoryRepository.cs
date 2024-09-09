using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CategoryDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// class CategoryRepository
    /// </summary>

    public class CategoryRepository: ICategoryRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

        /// <summary>
        ///  CategoryRepository constructor
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext"></param>

        public CategoryRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }

        /// <summary>
        /// AddCategory
        /// </summary>
        /// <param name="categoryName"></param>

        public void AddCategory(string categoryName)
        {
            var existingCategory = virtualShoppingStoreDbContext.Categories.FirstOrDefault(C=>C.CategoryName==categoryName);

            if (existingCategory != null)
            {
                throw new CustomException("Category with this name already exists.", 400);
            }

            var newCategory = new Category
            {
                CategoryName = categoryName
            };

            virtualShoppingStoreDbContext.Categories.Add(newCategory);
            virtualShoppingStoreDbContext.SaveChanges();
        }

        /// <summary>
        /// DeleteCategoryById
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteCategoryById(int id)
        {

            var data= virtualShoppingStoreDbContext.Categories.FirstOrDefault(x=>x.CategoryId==id);

            if (data == null) 
            {

                throw new CustomException("Invalid category Id",400);

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
        /// GetAllCategories
        /// </summary>
        /// <returns></returns>

        public List<Category> GetAllCategories()
        {
           
            var categorydata = virtualShoppingStoreDbContext.Categories.ToList();

            if (categorydata.IsNullOrEmpty())
            {
                throw new CustomException("Categories not found.", 404);
            }

            else
            {
                return categorydata;
            }

        }

        /// <summary>
        /// GetCategoryById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    
        public Category GetCategoryById(int id) 
        {
            
            var data= virtualShoppingStoreDbContext.Categories.FirstOrDefault(x=>x.CategoryId==id);

            if (data == null) 
            {

               throw new CustomException($"No such category found by this id:{id}",400);

            }

            return data;

        }

        /// <summary>
        /// UpdateCategory
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="updateCategoryDto"></param>
        
        public void UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto)
        {

            var category = virtualShoppingStoreDbContext.Categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (category == null)
            {

                throw new CustomException("Category not found.",400);

            }

            if(updateCategoryDto.CategoryName.Length==0)
            {
                throw new CustomException("category name must be atleast one character long");
            }

            category.CategoryName = updateCategoryDto.CategoryName ?? category.CategoryName;
            
            virtualShoppingStoreDbContext.Categories.Update(category);
            virtualShoppingStoreDbContext.SaveChanges();

        }

    }

}
