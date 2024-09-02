using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CartItemDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SQLCartItemRepository:ICartItemRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext"></param>
        public SQLCartItemRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Cartitem> GetAllUsersCartItems()
        {

            var allcartitems = virtualShoppingStoreDbContext.Cartitems.ToList();

            if (allcartitems != null)
            {
                return allcartitems;
            }

            else
            {
                throw new Exception("Cart Items table is empty");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Cartitem GetCartItemsByUserId(int id)
        {

            var cartitems= virtualShoppingStoreDbContext.Cartitems.FirstOrDefault(x => x.UserId == id);

            if (cartitems != null) 
            {
                return cartitems;
            }

                throw new Exception("Cart item with this user id does not exist");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addToCartDto"></param>
        public void AddItemToCart(AddToCartDto addToCartDto)
        {
            
            if (addToCartDto == null)
            {
                throw new Exception(nameof(addToCartDto));
            }
            
            var userExists = virtualShoppingStoreDbContext.Users.Any(x => x.UserId == addToCartDto.UserId);
            if (!userExists)
            {
                throw new Exception("User ID is invalid");
            }

            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(p => p.ProductId == addToCartDto.ProductId);

            if (product == null)
            {
                throw new Exception("Product ID is invalid");
            }

            if (product.StockQuantity < addToCartDto.Quantity)
            {
                throw new Exception("Stock is less than requested quantity");
            }
           
            var addtocart = new Cartitem()
            {
                Quantity = addToCartDto.Quantity,
                UserId = addToCartDto.UserId,
                ProductId = addToCartDto.ProductId,
               
                TotalAmount = product.Price * addToCartDto.Quantity 
            };

            virtualShoppingStoreDbContext.Cartitems.Add(addtocart);

            product.StockQuantity -= addToCartDto.Quantity;
            virtualShoppingStoreDbContext.Products.Update(product);

            virtualShoppingStoreDbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteProductQuantityFromCartByCartItemId(int cartItemId)
        {
            var cartItem = virtualShoppingStoreDbContext.Cartitems.FirstOrDefault(ci => ci.CartItemId == cartItemId);

            if (cartItem == null)
            {
                throw new Exception("Cart item not found.");
            }

            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(p => p.ProductId == cartItem.ProductId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            product.StockQuantity += cartItem.Quantity;
            virtualShoppingStoreDbContext.Products.Update(product);

            virtualShoppingStoreDbContext.Cartitems.Remove(cartItem);
            virtualShoppingStoreDbContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="updateCartDto"></param>
        /// <returns></returns>
        public Cartitem UpdateCartByProductId(int productId, UpdateCartDto updateCartDto)
        {
            var product = virtualShoppingStoreDbContext.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product == null)
            {
                throw new NotImplementedException("Invalid product id");
            }

            var cartitem = virtualShoppingStoreDbContext.Cartitems.FirstOrDefault(ci => ci.ProductId == productId && ci.UserId == updateCartDto.UserId);

            if (cartitem == null)
            {
                throw new Exception("Cart item not found.");
            }

            int quantitydifference = updateCartDto.Quantity - cartitem.Quantity;

            if (quantitydifference > 0 && product.StockQuantity<0)
            {
                throw new Exception("Product not found. It may have been removed.");    
            }

            cartitem.Quantity = updateCartDto.Quantity;
            cartitem.TotalAmount = product.Price * updateCartDto.Quantity;

            virtualShoppingStoreDbContext.Cartitems.Update(cartitem);
            virtualShoppingStoreDbContext.Products.Update(product);
            virtualShoppingStoreDbContext.SaveChanges();
            return cartitem;
        }
    }
}
