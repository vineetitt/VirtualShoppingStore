using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.UserDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    
    public interface IUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        List<User> GetAllUsers(int pageNumber , int pageSize );

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        User GetUserbyId(int id);

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        
        void AddUser(User user);

        /// <summary>
        /// Delete User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        void DeleteUserById(int id);

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserRequestDto"></param>
        /// <returns></returns>

        public User UpdateUserByPatch(int id, UpdateUserRequestDto updateUserRequestDto);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserByUserName(string userName);



        //public void SignUpUser(User newUser);

    }

}
