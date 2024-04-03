using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealWorldApp.Core.Tags;


namespace RealWorldApp.Infrastructure.DAL
{
    public class DataContext(DbContextOptions options) : DbContext(options) 
    {
        public virtual DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
