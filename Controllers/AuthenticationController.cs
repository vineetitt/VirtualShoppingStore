using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VirtualShoppingStore;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.LoginDto;
using VirtualShoppingStore.Models.DTO.UserDto.UserDto;
using VirtualShoppingStore.Repositories;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly JwtTokenService _tokenService;

    private readonly VirtualShoppingStoreDbContext virtualShoppingStoreDbContext;

    private readonly IConfiguration configuration;

    private readonly IUserRepository _userRepository;

    public AuthenticationController(JwtTokenService tokenService, VirtualShoppingStoreDbContext virtualShoppingStoreDbContext, IConfiguration configuration, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        this.virtualShoppingStoreDbContext = virtualShoppingStoreDbContext;
        this.configuration = configuration;
        this._userRepository = userRepository;
    }


    private string GetHashedPassword(string password)
    {
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(configuration["Hash:Secret"]!));
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password!);
        byte[] hashBytes = hmac.ComputeHash(passwordBytes);

        return Convert.ToBase64String(hashBytes);
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto loginRequest)
    {
        try
        {
            var hashedpassword = GetHashedPassword(loginRequest.Password);

            var user = _userRepository.GetUserByUserName(loginRequest.Username);

            if (user != null && user.PasswordHash == hashedpassword)
            {
                var token = _tokenService.GenerateToken(loginRequest.Username);
                return Ok(new { Token = token, userId = user.UserId });
            }

            return Unauthorized();
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



    [HttpPost("signup")]

    public IActionResult SignUpUser(AddUserDto newUserDto)
        {
        var hashedPassword = GetHashedPassword(newUserDto.PasswordHash);

        var newUser = new User
        {
            Username = newUserDto.Username,
            Email = newUserDto.Email,
            PasswordHash = hashedPassword,
        };

        try
        {
            _userRepository.AddUser(newUser);
            return Ok();
        }
        catch(CustomException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
