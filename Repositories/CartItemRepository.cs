using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CartItemDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    
    public class CartItemRepository:ICartItemRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

        /// <summary>
        /// CartItemRepository constructor
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext"></param>
        public CartItemRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }

        /// <summary>
        /// Get Cart By UserId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<Cartitem> GetCartByUserId(int id)
        {

            var cartitems= virtualShoppingStoreDbContext.Cartitems
                 .Include(ci => ci.Product)
                    .ThenInclude(p=>p.Category) 
                 //.Include(ci => ci.User)
                .Where(x => x.UserId == id && x.IsPlaced==false).ToList();


            if (cartitems != null) 
            {
                return cartitems;
            }

            throw new CustomException($"No non placed cart item found for user ID {id}", 400);

        }

        /// <summary>
        /// Add To Cart
        /// </summary>
        /// <param name="addToCartDto"></param>
        
        public void AddToCart(AddToCartDto addToCartDto)
        {

            if (addToCartDto == null)
            {
                throw new CustomException("Cart item data must be provided.", 400);
            }

            var userExists = virtualShoppingStoreDbContext.Users.Any(x => x.UserId == addToCartDto.UserId);
            if (!userExists)
            {
                throw new CustomException("User ID is invalid", 400);
            }

            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(p => p.ProductId == addToCartDto.ProductId);

            if (product == null)
            {
                throw new CustomException("Product ID is invalid", 400);
            }

            if (product.StockQuantity < addToCartDto.Quantity)
            {
                throw new CustomException("Stock is less than requested quantity", 400);
            }

            var existingcartitem = virtualShoppingStoreDbContext.Cartitems.FirstOrDefault(ui=>ui.UserId == addToCartDto.UserId && ui.ProductId== addToCartDto.ProductId);

            if (existingcartitem != null) { 
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
        /// Delete From Cart BY CartitemId
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <exception cref="Exception"></exception>
        
        public void DeleteFromCart(int cartItemId)
        {
            var cartItem = virtualShoppingStoreDbContext.Cartitems.FirstOrDefault(ci => ci.CartItemId == cartItemId);

            if (cartItem == null)
            {
                throw new CustomException("Cart item not found.",400);
            }

            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(p => p.ProductId == cartItem.ProductId);

            if (product == null)
            {
                throw new CustomException("Product not found.",400);
            }

            virtualShoppingStoreDbContext.Cartitems.Remove(cartItem);
            virtualShoppingStoreDbContext.SaveChanges();
        }

        /// <summary>
        /// Update Cart item by cartitem Id 
        /// </summary>
        /// <param name="cartitemId"></param>
        /// <param name="updateCartDto"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>

        public Cartitem UpdateCart(int cartitemId, UpdateCartDto updateCartDto)
        {
            var cartitemid = virtualShoppingStoreDbContext.Cartitems.FirstOrDefault(x => x.CartItemId == cartitemId);

            if (cartitemid == null) {
                throw new CustomException("Cart item id is invalid");
            }

            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(x => x.ProductId == cartitemid.ProductId);

            if(product == null)
            {
                throw new CustomException("Product not found it might have been deleted or out of stock as of now", 400);
            }

            cartitemid.Quantity= updateCartDto.Quantity;
            cartitemid.TotalAmount =  updateCartDto.Quantity* product.Price;

            virtualShoppingStoreDbContext.SaveChanges();

            return cartitemid;
        }

    }

}
