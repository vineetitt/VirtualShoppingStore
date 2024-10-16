using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.ProductDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// Defines the contract for product-related operations in the VirtualShoppingStore repository.
    /// </summary>

    public interface IProductRepository
    {

        /// <summary>
        /// Retrieves a paginated list of products with optional filtering.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageNumber">The page number for pagination.</param>
        /// <param name="pagesize">The number of products per page.</param>
        /// <returns>A list of products matching the criteria.</returns>
        /// <exception cref="CustomException">Thrown when no products are found.</exception>

        List<Product> GetAllProducts(int? categoryId, int pageNumber, int pagesize);

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="addProductDto">The data transfer object containing the details of the product to add.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <exception cref="CustomException">Thrown when the category is not found or the product already exists.</exception>

        void AddNewProduct(AddProductDto addProductDto);

        /// <summary>
        /// Deletes a product by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <exception cref="CustomException">Thrown when no product is found with the given ID.</exception>

        void DeleteProductById(int id);

        /// <summary>
        /// Updates the details of a product identified by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the product to update.</param>
        /// <param name="patchProductDto">The data transfer object containing updated product details.</param>
        /// <returns>The updated product.</returns>
        /// <exception cref="CustomException">Thrown when no product is found with the given ID.</exception>

        public Product UpdateProductById(int id, PatchProductDto patchProductDto );

        /// <summary>
        /// Retrieves a product by its unique identifier.
        /// </summary>
        /// <param name="productId">The unique identifier of the product to retrieve.</param>
        /// <returns>The product with the given ID.</returns>
        /// <exception cref="CustomException">Thrown when no product is found with the given ID.</exception>

        public Product GetProductById(int productId);

        /// <summary>
        /// Get Product Count
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int GetProductCount(int? categoryId);

    }

}
