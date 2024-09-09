namespace VirtualShoppingStore.Models.DTO.UserDto
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateUserRequestDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string ?Username { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public string ?FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ?PhoneNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ?Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ?City { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public string ?PasswordHash { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public string ?Email { get; set; }

    }
}
