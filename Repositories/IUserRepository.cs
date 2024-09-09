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
        List<User> GetAllUser();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        User GetusersbyId(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        void AddUser(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteUserById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserRequestDto"></param>
        /// <returns></returns>
        public User UpdateUserByPatch(int id, UpdateUserRequestDto updateUserRequestDto);

    }

}
