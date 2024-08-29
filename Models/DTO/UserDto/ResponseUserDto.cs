namespace VirtualShoppingStore.Models.DTO.UserDto
{
    /// <summary>
    /// Dto for get users
    /// </summary>
    public class ResponseUserDto
    {
        /// <summary>
        /// 
        /// </summary>
        
        public int UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public bool? Deactive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? PhoneNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatedAt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? City { get; set; }


        public string MaskedPassword => "******";


    }
}
