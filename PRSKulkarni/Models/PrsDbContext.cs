using Microsoft.EntityFrameworkCore;
using PRSKulkarni.Models;

namespace PRSKulkarni.Models
{
    public class PrsDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Product>? Product { get; set; }

        public DbSet<Request> Requests { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        //constructor to support dependency injection
        public PrsDbContext(DbContextOptions<PrsDbContext> options) : base(options)
        {

        }
        //constructor to support dependency injection
        
    }
}
