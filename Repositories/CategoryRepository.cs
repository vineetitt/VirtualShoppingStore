using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CategoryDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// Repository class for managing category data in the VirtualShoppingStore application.
    /// </summary>

    public class CategoryRepository: ICategoryRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext">The database context used for category management.</param>

        public CategoryRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }

        /// <summary>
        /// Adds a new category to the database.
        /// </summary>
        /// <param name="categoryName">The name of the category to add.</param>
        /// <exception cref="CustomException">Thrown if a category with the same name already exists.</exception>

        public void AddCategory(string categoryName)
        {
            var existingCategory = virtualShoppingStoreDbContext.Categories.FirstOrDefault(category=>category.CategoryName==categoryName);

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
        /// Deletes a category from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <exception cref="CustomException">Thrown if the category does not exist or cannot be deleted.</exception>
        
        public void DeleteCategoryById(int id)
        {

            try
            {
                virtualShoppingStoreDbContext.Database.ExecuteSqlInterpolated($"EXEC DeleteCategoryById {id}");
            }
            catch (SqlException sqlEx)
            {

                if (sqlEx.Message.Contains("NO SUCH CATEGORY FOUND WITH THIS ID"))
                {
                    throw new CustomException("No such category found with this id", 404);
                }

                else if (sqlEx.Message.Contains("CANNOT DELETE BCOZ PRODUCT IS PRESENT WITH THIS ID"))
                {
                    throw new CustomException("Cannot delete because product is present with this id", 400);
                }

                else
                {
                    throw new Exception("An error occurred while deleting the category.");
                }

            }

            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>A list of <see cref="Category"/> objects representing all categories.</returns>
        /// <exception cref="CustomException">Thrown if no categories are found.</exception>

        public List<Category> GetAllCategories()
        {
           
            var categorydata = virtualShoppingStoreDbContext.Categories.ToList();

            if (!categorydata.Any())
            {
                throw new CustomException("Categories not found.", 404);
            }

            else
            {
                return categorydata;
            }

        }

        /// <summary>
        /// Retrieves a specific category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>A <see cref="Category"/> object representing the specified category.</returns>
        /// <exception cref="CustomException">Thrown if the category is not found.</exception>

        public Category GetCategoryById(int id) 
        {
            
            var categoryData= virtualShoppingStoreDbContext.Categories.FirstOrDefault(category=>category.CategoryId==id);

            if (categoryData == null)
            {
                throw new CustomException($"Category not found by this id:{id}", 404);
            }

            return categoryData;

        }

        /// <summary>
        /// Updates an existing category by its ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to update.</param>
        /// <param name="updateCategoryDto">The data to update the category with.</param>
        /// <exception cref="CustomException">Thrown if the category is not found or the name is invalid.</exception>

        public void UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto)
        {

            var category = virtualShoppingStoreDbContext.Categories.FirstOrDefault(category => category.CategoryId == categoryId);

            if (category == null)
            {

                throw new CustomException("Category not found.",404);

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
