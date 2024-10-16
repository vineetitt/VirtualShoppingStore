using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.ProductDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// Repository class for managing products in the VirtualShoppingStore database.
    /// </summary>

    public class ProductRepository:IProductRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext">The database context for accessing products.</param>

        public ProductRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }

        /// <summary>
        /// Retrieves a paginated list of products with optional filtering.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageNumber">The page number for pagination.</param>
        /// <param name="pagesize">The number of products per page.</param>
        /// <returns>A list of products matching the criteria.</returns>
        /// <exception cref="CustomException">Thrown when no products are found.</exception>

        public List<Product> GetAllProducts(int? categoryId, int pageNumber, int pagesize)
        {
            
            var data= virtualShoppingStoreDbContext.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                data = data.Where(p => p.CategoryId == categoryId);
            }
            var size= (pageNumber-1)*pagesize;

            if (data.Any())
            {
                return data.Skip(size).Take(pagesize).ToList();
            }

            throw new CustomException("No products found.", 204);
        }

        /// <summary>
        /// Adds a new product to the database.
        /// </summary>
        /// <param name="addProductDto">The data transfer object containing product details.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <exception cref="CustomException">Thrown when the category is not found or the product already exists.</exception>

        public void AddNewProduct(AddProductDto addProductDto)
        {
            var category = virtualShoppingStoreDbContext.Categories.FirstOrDefault(cat => cat.CategoryId == addProductDto.CategoryId);

            if (category == null)
            {
                throw new CustomException("Category not found", 400);
            }

            var existingProduct = virtualShoppingStoreDbContext.Products.FirstOrDefault(prod=>prod.ProductName.Equals(addProductDto.ProductName, StringComparison.OrdinalIgnoreCase));

            if (existingProduct != null)
            {
                throw new CustomException("Product with this name already exists. Try another name", 400);
            }

            var product = new Product()
            {
                ProductName = addProductDto.ProductName,
                Description = addProductDto.Description,
                Price = addProductDto.Price,
                StockQuantity = addProductDto.StockQuantity,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsDeleted = false
            };

            virtualShoppingStoreDbContext.Products.Add(product);
            virtualShoppingStoreDbContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        /// <exception cref="CustomException">Thrown when no product is found with the given ID.</exception>

        public void DeleteProductById(int id)
        {
            var productToDelete = virtualShoppingStoreDbContext.Products.FirstOrDefault(prod => prod.ProductId == id);

            if (productToDelete == null)
            {
                throw new CustomException("No product available by this id",404);
            }

            virtualShoppingStoreDbContext.Products.Remove(productToDelete);
            virtualShoppingStoreDbContext.SaveChanges();
            
        }

        /// <summary>
        /// Updates a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="patchProductDto">The data transfer object containing updated product details.</param>
        /// <returns>The updated product.</returns>
        /// <exception cref="CustomException">Thrown when no product is found with the given ID.</exception>

        public Product UpdateProductById(int id, PatchProductDto patchProductDto)
        {
            
            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(prod => prod.ProductId == id);

            if (product == null) {
                throw new CustomException("No product found with this product id", 404);
            }

            product.ProductName = patchProductDto.ProductName ?? product.ProductName;
            product.Description = patchProductDto.Description ?? product.Description;
            product.Price = patchProductDto.Price ?? product.Price;
            product.StockQuantity = patchProductDto.StockQuantity ?? product.StockQuantity;
            product.CategoryId = patchProductDto.CategoryId ?? product.CategoryId;
            product.IsDeleted = patchProductDto.IsDeleted ?? product.IsDeleted;

            virtualShoppingStoreDbContext.SaveChanges();

            return product;

        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve.</param>
        /// <returns>The product with the given ID.</returns>
        /// <exception cref="CustomException">Thrown when no product is found with the given ID.</exception>

        public Product GetProductById(int productId)
        {
            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(prod => prod.ProductId == productId);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new CustomException("Product not found.",404);
            }

        }

        /// <summary>
        /// Get Product Count
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public int GetProductCount(int? categoryId)
        {
            var data = virtualShoppingStoreDbContext.Products.AsQueryable();

            if (categoryId.HasValue)
            {
                data = data.Where(p => p.CategoryId == categoryId);
            }

            return data.Count();
        }

    }

}
