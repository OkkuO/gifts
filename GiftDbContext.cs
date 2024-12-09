
using Microsoft.EntityFrameworkCore;

namespace snow_bot
{
    public class GiftDbContext : DbContext
    {
        public GiftDbContext(DbContextOptions<GiftDbContext> options)
            : base(options)
        {
            GiftModels = Set<GiftModel>();    
        }  

        public DbSet <GiftModel> GiftModels { get; set; }
    }
}