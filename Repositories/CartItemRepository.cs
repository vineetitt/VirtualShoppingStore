using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CartItemDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// Repository for managing cart items in the virtual shopping store.
    /// </summary>

    public class CartItemRepository:ICartItemRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartItemRepository"/> class.
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext">The database context used to manage cart items.</param>
        public CartItemRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }

        /// <summary>
        /// Retrieves cart items for a specific user that are not yet placed.
        /// </summary>
        /// <param name="id">The ID of the user whose cart items are to be retrieved.</param>
        /// <returns>A collection of non-placed cart items for the specified user.</returns>
        /// <exception cref="CustomException">Thrown when no non-placed cart items are found for the user.</exception>
        public IEnumerable<Cartitem> GetCartByUserId(int id)
        {

            var cartitems= virtualShoppingStoreDbContext.Cartitems
                 .Include(cartitem => cartitem.Product)
                    .ThenInclude(p=>p.Category) 
                .Where(cartitem => cartitem.UserId == id && cartitem.IsPlaced==false).ToList();


            if (cartitems != null) 
            {
                return cartitems;
            }

            throw new CustomException($"No non placed cart item found for user ID {id}", 400);

        }

        /// <summary>
        /// Adds a product to the cart for a specific user.
        /// </summary>
        /// <param name="addToCartDto">The DTO containing the details of the product to be added to the cart.</param>
        /// <exception cref="CustomException">Thrown for invalid user ID, invalid product ID, insufficient stock, or null DTO.</exception>

        public void AddToCart(AddToCartDto addToCartDto)
        {

            if (addToCartDto == null)
            {
                throw new CustomException("Cart item data must be provided.", 400);
            }

            var userExists = virtualShoppingStoreDbContext.Users.Any(user => user.UserId == addToCartDto.UserId);
            if (!userExists)
            {
                throw new CustomException("User ID is invalid", 400);
            }

            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(product => product.ProductId == addToCartDto.ProductId);

            if (product == null)
            {
                throw new CustomException("Product ID is invalid", 400);
            }

            if (product.StockQuantity < addToCartDto.Quantity)
            {
                throw new CustomException("Stock is less than requested quantity", 400);
            }

            var existingcartitem = virtualShoppingStoreDbContext.Cartitems.FirstOrDefault(cartitem=>cartitem.UserId == addToCartDto.UserId && cartitem.ProductId== addToCartDto.ProductId);

            if (existingcartitem != null)
            { 
                existingcartitem.Quantity += addToCartDto.Quantity;
                existingcartitem.TotalAmount= existingcartitem.Quantity*product.Price;
            }

            else
            {
                var addtocart = new Cartitem()
                {
                    Quantity = addToCartDto.Quantity,
                    UserId = addToCartDto.UserId,
                    ProductId = addToCartDto.ProductId,

                    TotalAmount = product.Price * addToCartDto.Quantity
                };

                virtualShoppingStoreDbContext.Cartitems.Add(addtocart);

            }

            virtualShoppingStoreDbContext.SaveChanges();

        }

        /// <summary>
        /// Deletes a cart item by its ID.
        /// </summary>
        /// <param name="cartItemId">The ID of the cart item to be deleted.</param>
        /// <exception cref="CustomException">Thrown if the cart item or its associated product is not found.</exception>

        public void DeleteFromCart(int cartItemId)
            {
                var cartItem = virtualShoppingStoreDbContext.Cartitems.FirstOrDefault(cartitem => cartitem.CartItemId == cartItemId);

                if (cartItem == null)
                {
                    throw new CustomException("Cart item not found.",404);
                }

                var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(product => product.ProductId == cartItem.ProductId);

                if (product == null)
                {
                    throw new CustomException("Product not found.",404);
                }

                virtualShoppingStoreDbContext.Cartitems.Remove(cartItem);
                virtualShoppingStoreDbContext.SaveChanges();
            }

        /// <summary>
        /// Updates the quantity of a cart item by its ID.
        /// </summary>
        /// <param name="cartitemId">The ID of the cart item to be updated.</param>
        /// <param name="updateCartDto">The DTO containing updated quantity and other details.</param>
        /// <returns>The updated cart item.</returns>
        /// <exception cref="CustomException">Thrown if the cart item or its associated product is not found.</exception>

        public Cartitem UpdateCart(int cartitemId, UpdateCartDto updateCartDto)
        {
            var cartitemid = virtualShoppingStoreDbContext.Cartitems.FirstOrDefault(cartitem => cartitem.CartItemId == cartitemId);

            if (cartitemid == null) {
                throw new CustomException("Cart item id is invalid",400);
            }

            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(product => product.ProductId == cartitemid.ProductId);

            if(product == null)
            {
                throw new CustomException("Product not found it might have been deleted or out of stock as of now", 404);
            }

            cartitemid.Quantity= updateCartDto.Quantity;
            cartitemid.TotalAmount =  updateCartDto.Quantity* product.Price;

            virtualShoppingStoreDbContext.SaveChanges();

            return cartitemid;
        }

    }

}
