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
        /// Get All User
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        
        public List<User> GetAllUsers(int pageNumber = 1,  int pageSize = 50)
        {
            var recordsToSkip = (pageNumber - 1) * pageSize; 

            var filteredUsers = virtualShoppingStoreDbContext.Users.Where(user => user.Deactive == false).Skip(recordsToSkip).Take(pageSize).ToList();

            if (!filteredUsers.Any())
            {
                throw new CustomException("No Users found", 404);
            }

            return filteredUsers;

        }

        /// <summary>
        /// Get User By id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public User GetUserbyId(int id)
        {
            var user = virtualShoppingStoreDbContext.Users.FirstOrDefault(user => user.Deactive == false && user.UserId == id);

            if (user==null) 
            {
                throw new CustomException("User not found",404);
            }
            return user;

        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>

        public void AddUser(User newUser)
        {
            var existingUser = virtualShoppingStoreDbContext.Users.FirstOrDefault(user=> user.Email == newUser.Email || user.Username == newUser.Username);

            if (existingUser==null)
            {
                virtualShoppingStoreDbContext.Users.Add(newUser);
                virtualShoppingStoreDbContext.SaveChanges();
            }
            else
            {

                if (existingUser.Deactive==true){

                    existingUser.Deactive = false;
                    virtualShoppingStoreDbContext.SaveChanges();

                }
                else
                {
                    throw new CustomException("User already exists", 400);
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
            var userToDelete = GetUserbyId(id);
            userToDelete.Deactive = true;
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

            var userToUpdate = virtualShoppingStoreDbContext.Users.Find(id);

            if (userToUpdate == null)
            {
                throw new CustomException("User not found", 400);
            }

            userToUpdate.Username = !string.IsNullOrEmpty(updateUserRequestDto.Username) ? updateUserRequestDto.Username : userToUpdate.Username;
            userToUpdate.FirstName = !string.IsNullOrEmpty(updateUserRequestDto.FirstName) ? updateUserRequestDto.FirstName : userToUpdate.FirstName;
            userToUpdate.LastName = !string.IsNullOrEmpty(updateUserRequestDto.LastName) ? updateUserRequestDto.LastName : userToUpdate.LastName;
            userToUpdate.Email = !string.IsNullOrEmpty(updateUserRequestDto.Email) ? updateUserRequestDto.Email : userToUpdate.Email;
            userToUpdate.City = !string.IsNullOrEmpty(updateUserRequestDto.City) ? updateUserRequestDto.City : userToUpdate.City;
            userToUpdate.Address = !string.IsNullOrEmpty(updateUserRequestDto.Address) ? updateUserRequestDto.Address : userToUpdate.Address;
            userToUpdate.PasswordHash = !string.IsNullOrEmpty(updateUserRequestDto.PasswordHash) ? updateUserRequestDto.PasswordHash : userToUpdate.PasswordHash;
            userToUpdate.PhoneNo = !string.IsNullOrEmpty(updateUserRequestDto.PhoneNo) ? updateUserRequestDto.PhoneNo : userToUpdate.PhoneNo;

            virtualShoppingStoreDbContext.SaveChanges();
            return userToUpdate;

        }

    }

}
