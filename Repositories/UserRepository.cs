using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.UserDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// UserRepository class
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualShoppingStoreDbContext"></param>
        public UserRepository(VirtualShoppingStoreDbContext virtualShoppingStoreDbContext)
        {
            this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        }

        /// <summary>
        /// Get all User
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        
        public List<User> GetAllUser()
        {
            var allUserData = virtualShoppingStoreDbContext.Users.Where(e => e.Deactive == false).ToList();


            if (!allUserData.Any())
            {
                throw new CustomException("No Users found",400);
            }

            return allUserData;

        }

        /// <summary>
        /// Get User By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetusersbyId(int id)
        {
            var users = virtualShoppingStoreDbContext.Users.FirstOrDefault(a=>a.Deactive == false && a.UserId == id);
            if (users==null) {
                throw new CustomException("No User found with this id",404);
            }
            return users;
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public void AddUser(User user)
        {
            var exisitingUser = virtualShoppingStoreDbContext.Users.FirstOrDefault(a=>a.Email == user.Email || a.Username == user.Username);
            if (exisitingUser==null)
            {

                virtualShoppingStoreDbContext.Users.Add(user);
                virtualShoppingStoreDbContext.SaveChanges();
            }
            else
            {
                if (exisitingUser.Deactive==true){
                    exisitingUser.Deactive = false;
                    virtualShoppingStoreDbContext.SaveChanges();
                }
                else
                {
                    throw new CustomException("User with this email or username already exists",400);
                }
                
            }
        }

        /// <summary>
        /// Delete User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public void DeleteUserById(int id)
        {
            var existingUser = GetusersbyId(id);
            existingUser.Deactive = true;
            virtualShoppingStoreDbContext.SaveChanges();
        }

        /// <summary>
        /// UpdateUserByPatch
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public User UpdateUserByPatch(int id, UpdateUserRequestDto updateUserRequestDto )
        {

            var existinguser = virtualShoppingStoreDbContext.Users.Find(id);

            if (existinguser == null)
            {
                throw new CustomException("No existing user found with this id",400);
            }

            existinguser.Username = updateUserRequestDto.Username ?? existinguser.Username;
            existinguser.FirstName = updateUserRequestDto.FirstName ?? existinguser.FirstName;
            existinguser.LastName = updateUserRequestDto.LastName ?? existinguser.LastName;
            existinguser.Email = updateUserRequestDto.Email ?? existinguser.Email;
            existinguser.City = updateUserRequestDto.City ?? existinguser.City;
            existinguser.Address = updateUserRequestDto.Address ?? existinguser.Address;
            existinguser.PasswordHash = updateUserRequestDto.PasswordHash ?? existinguser.PasswordHash;
            existinguser.PhoneNo = updateUserRequestDto.PhoneNo ?? existinguser.PhoneNo;

            virtualShoppingStoreDbContext.SaveChanges();
            return existinguser;

        }

    }

}
