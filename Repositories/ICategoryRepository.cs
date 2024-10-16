using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CategoryDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// Interface for category repository operations.
    /// </summary>

    public interface ICategoryRepository
    {

        /// <summary>
        /// Retrieves all categories.
        /// </summary>
        /// <returns>A list of <see cref="Category"/> objects representing all categories.</returns>

        public List<Category> GetAllCategories();


        /// <summary>
        /// Retrieves a category by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category.</param>
        /// <returns>A <see cref="Category"/> object representing the category with the specified identifier.</returns>

        public Category GetCategoryById(int id);

        /// <summary>
        /// Deletes a category by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the category to be deleted.</param>
        /// <returns>Returns a <see cref="void"/>. Throws an exception if the category cannot be found or is associated with products.</returns>

        public void DeleteCategoryById(int id);

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="categoryName">The name of the category to be added.</param>
        /// <returns>Returns a <see cref="void"/>. Throws an exception if the category already exists.</returns>

        public void AddCategory(string categoryName);


        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="categoryId">The unique identifier of the category to be updated.</param>
        /// <param name="updateCategoryDto">An <see cref="UpdateCategoryDto"/> object containing the updated category details.</param>
        /// <returns>Returns a <see cref="void"/>. Throws an exception if the category cannot be found or if the update fails.</returns>

        public void UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto);

    }

}
