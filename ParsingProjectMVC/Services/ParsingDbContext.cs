using Microsoft.EntityFrameworkCore;
using ParsingProjectMVC.Models;

namespace ParsingProjectMVC.Services
{
    public class ParsingDbContext : DbContext
    {
        public ParsingDbContext(DbContextOptions<ParsingDbContext> options) : base(options)
        {
        
        }

        public DbSet<SahaModel> Saha { get; set; }  
        public DbSet<KuyuGrubuModel> KuyuGrubu { get; set; }
        public DbSet<KuyuModel> Kuyu { get; set; } 
        public DbSet<WellboreModel> Wellbore { get; set; }
    }
}
