using Microsoft.EntityFrameworkCore;
using Head_brands.web.Models;

namespace Head_brands.web.Data
{
    public class HeadIndexContext : DbContext
    {
        public HeadIndexContext(DbContextOptions<HeadIndexContext> options) : base(options)
        {
        }
        public DbSet<HeadModel> Heads { get; set; }   
    }
}