using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VirtualShoppingStore.Models;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SQLUserRepository : IUserRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext"></param>
        public SQLUserRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }





        public async Task<User> AddUserAsync(User user)
        {
             await virtualShoppingStoreDbContext.Users.AddAsync(user);  
            await virtualShoppingStoreDbContext.SaveChangesAsync(); 
            return user;
        }

        public async Task<User?> DeleteUserByIdAsync(int id)
        {
            var existingUser= await virtualShoppingStoreDbContext.Users.FirstOrDefaultAsync(x=>x.UserId==id);
            if (existingUser == null) {
                return null;
            }

             virtualShoppingStoreDbContext.Users.Remove(existingUser);
             await virtualShoppingStoreDbContext.SaveChangesAsync();
            return existingUser;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<User>> GetAllUserAsync()
        {
            
            return await virtualShoppingStoreDbContext.Users.Where(e => e.Deactive == false).ToListAsync();
        }

        public async Task<User?> getusersbyIdAsync(int id)
        {
            return await virtualShoppingStoreDbContext.Users.FirstOrDefaultAsync(x=>x.UserId == id);
        }


        public async Task<User?> UpdateUserDetailAsync(int id, User user)
        {
            var existingUser =await virtualShoppingStoreDbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (existingUser == null) {
                return null;
            }
            existingUser.UserId = id;
            existingUser.PhoneNo = user.PhoneNo;
            existingUser.Email = user.Email;
            existingUser.City = user.City;
            existingUser.Address = user.Address;
            existingUser.Username = user.Username;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;

            await virtualShoppingStoreDbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}
