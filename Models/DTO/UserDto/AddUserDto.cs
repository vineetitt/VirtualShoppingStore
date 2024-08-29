using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models.DTO.UserDto.UserDto
{
    /// <summary>
    /// DTO for User Addition
    /// </summary>
    public class AddUserDto
    {
        /// <summary>
        /// User Name
        /// </summary>
        

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 100 characters.")]
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        /// <summary>
        /// User Email
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; } = null!;

        /// <summary>
        /// User's First Name
        /// </summary>
        [StringLength(50, ErrorMessage = "First name can't be longer than 50 characters.")]
        public string? FirstName { get; set; }

        /// <summary>
        /// User's Last Name
        /// </summary>
        [StringLength(50, ErrorMessage = "Last name can't be longer than 50 characters.")]
        public string? LastName { get; set; }
        /// <summary>
        /// User's Phone number
        /// </summary>
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNo { get; set; }

        /// <summary>
        /// Created AT
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// User's Address
        /// </summary>
        [StringLength(200, ErrorMessage = "Address can't be longer than 200 characters.")]
        public string? Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, and one digit.")]
        public string PasswordHash { get; set; } = null!;
        /// <summary>
        /// User's City
        /// </summary>
        [StringLength(100, ErrorMessage = "City name can't be longer than 100 characters.")]
        public string? City { get; set; }
    }
}
