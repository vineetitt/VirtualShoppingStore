using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
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
        private readonly IConfiguration configuration;
        private string GetHashedPassword(string password)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(configuration["Hash:Secret"]!));
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password!);
            byte[] hashBytes = hmac.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashBytes);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>

        public UserController(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        /// <summary>
        /// Retrieves all users with pagination.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (default is 1).</param>
        ///  <param name="pageSize">The number of users per page (default is 5).</param>
        /// <returns></returns>
        
        [HttpGet]
        
        public IActionResult GetallUsers(int pageNumber=1, int pageSize=5)
        {
            try
            {
                var userList = userRepository.GetAllUsers(pageNumber, pageSize);
                return Ok(userList);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        ///  Retrieves a user by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        //[HttpGet]
        //[Route("{id}")]

        //public IActionResult GetUserbyId(int id)
        //{
        //    try
        //    {
        //        var retrievedUser = userRepository.GetUserbyId(id);
        //        return Ok(retrievedUser);
        //    }

        //    catch (CustomException ex)
        //    {
        //        return StatusCode(ex.StatusCode, ex.Message);
        //    }

        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="addUserDto"></param>
        /// <returns></returns>

        [HttpPost]

        public IActionResult AddUser([FromBody] AddUserDto addUserDto)
        {

            var hashedPassword = GetHashedPassword(addUserDto.PasswordHash);
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
                    PasswordHash = hashedPassword,

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
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Deletes a user by ID.
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
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Updates a user's details via PATCH.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserRequestDto"></param>
        /// <returns></returns>

        [HttpPatch]

        public IActionResult UpdateUserByPatch(int id, UpdateUserRequestDto updateUserRequestDto)
        {

            try
            {
                var updatedUser = userRepository.UpdateUserByPatch(id, updateUserRequestDto);
                return Ok(updatedUser);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// get user by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("{userName}")]

        public IActionResult GetUserByUserName([FromQuery]string userName)
        {


            try
            {
                var getUser = userRepository.GetUserByUserName(userName);
                return Ok(getUser);
            }
            catch (CustomException ex) 
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
