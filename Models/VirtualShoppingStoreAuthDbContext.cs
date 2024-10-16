using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VirtualShoppingStore.Models
{
    public class VirtualShoppingStoreAuthDbContext : IdentityDbContext
    {
        public VirtualShoppingStoreAuthDbContext(DbContextOptions<VirtualShoppingStoreAuthDbContext> options) : base(options)
        {
        }
    }
}
