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
        //private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;
        private readonly IUserRepository userRepository;

        /// <summary>
        /// User constructor
        /// </summary>
       // /// <param name="virtualShoppingStoreDbContext"></param>
        /// <param name="userRepository"></param>
        public UserController( IUserRepository userRepository)
        {
            //this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetallUser()
        {
            /// non delted user?
            var data = await userRepository.GetAllUserAsync();

            //doubt
            var responseUserDto = new List<ResponseUserDto>();
            foreach (var i in data)
            {
                responseUserDto.Add(new ResponseUserDto()
                {
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Email = i.Email,
                    PhoneNo = i.PhoneNo,
                    Address = i.Address,
                    City = i.City,
                    UserId = i.UserId,
                    CreatedAt = i.CreatedAt,
                   
                });
            }
            return Ok(responseUserDto);
        }




        /// <summary>
        /// Get User By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getusersbyId(int id)
        {
            var data = await userRepository.getusersbyIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            ResponseUserDto userdto = new ResponseUserDto()
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email

            };

            return Ok(userdto);
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="addUserDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto addUserDto)
        {
            User userdomain = new ()
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




            userdomain=await userRepository.AddUserAsync(userdomain);
           


            AddUserDto userDto = new()
            {
                FirstName = userdomain.FirstName,
                LastName = userdomain.LastName,
                Email = userdomain.Email,
                PhoneNo = userdomain.PhoneNo,
                Address = userdomain.Address,
                City = userdomain.City,
                Username = userdomain.Username,
            };

            return CreatedAtAction(nameof(getusersbyId), new { id = userdomain.UserId }, userDto);



        }

        /// <summary>
        /// Update User Information By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserRequestDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUserDetail(int id, UpdateUserRequestDto updateUserRequestDto)
        {
            //Map dto to domain model
            var userdomain = new User()
            {
                FirstName = updateUserRequestDto.FirstName,
                LastName = updateUserRequestDto.LastName,
                Email = updateUserRequestDto.Email,
                Address = updateUserRequestDto.Address,
                City = updateUserRequestDto.City,
                Username = updateUserRequestDto.Username,
                PhoneNo = updateUserRequestDto.PhoneNo,
                PasswordHash = updateUserRequestDto.PasswordHash,


            };
             userdomain = await userRepository.UpdateUserDetailAsync(id, userdomain);
            if (userdomain == null)
            {
                return NotFound();
            }


          

            

            UpdateUserRequestDto updateUserDetail = new ()
            {
                FirstName = userdomain.FirstName,
                LastName = userdomain.LastName,
                Email = userdomain.Email,
                City = userdomain.City,
                PasswordHash = userdomain.PasswordHash,
                PhoneNo = userdomain.PhoneNo,
                Address = userdomain.Address,
                Username = userdomain.Username,
            };
            return Ok(updateUserDetail);

        }

        /// <summary>
        ///  Delete User By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete]

        [Route("{id}")]

        public async Task<IActionResult> DeleteUserById(int id)
        {
            var userdomain = await userRepository.DeleteUserByIdAsync(id);
            if (userdomain == null)
            {
                return NotFound();
            }

             

            
            return Ok("Deleted Successfully");


        }

    }
}
