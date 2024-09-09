using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.UserDto;
using VirtualShoppingStore.Models.DTO.UserDto.UserDto;
using VirtualShoppingStore.Repositories;

namespace VirtualShoppingStore.Controllers
{
    /// <summary>
    /// User Contoller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly IUserRepository userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetallUser()
        {
            try
            {
                var response = userRepository.GetAllUser();
                return Ok(response);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// Get User By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet]
        [Route("{id}")]

        public IActionResult GetUsersbyId(int id)
        {
            try
            {
                var response = userRepository.GetusersbyId(id);
                return Ok(response);

            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="addUserDto"></param>
        /// <returns></returns>
        
        [HttpPost]

        public IActionResult AddUser([FromBody] AddUserDto addUserDto)
        {
            try
            {
                User userdomain = new()
                {
                    FirstName = addUserDto.FirstName,
                    LastName = addUserDto.LastName,
                    Email = addUserDto.Email,
                    PhoneNo = addUserDto.PhoneNo,
                    Address = addUserDto.Address,
                    City = addUserDto.City,
                    Username = addUserDto.Username,
                    PasswordHash = addUserDto.PasswordHash,

                };

                userRepository.AddUser(userdomain);
                return Ok(userdomain);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        /// <summary>
        ///  Delete User By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                userRepository.DeleteUserById(id);
                return Ok("Deleted Successfully");

            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// Update User detail
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserRequestDto"></param>
        /// <returns></returns>
        [HttpPatch]
        public IActionResult UpdateUserByPatch(int id, UpdateUserRequestDto updateUserRequestDto)
        {
            try
            {
                var updated = userRepository.UpdateUserByPatch(id, updateUserRequestDto);
                return Ok(updated);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }
    }
}
