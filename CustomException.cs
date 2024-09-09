namespace VirtualShoppingStore
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomException :Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// 

        public int StatusCode { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="StatusCode"></param>
        
        public CustomException(string message, int StatusCode=500 ): base(message)
        {
            this.StatusCode= StatusCode;
        }
    }

}
