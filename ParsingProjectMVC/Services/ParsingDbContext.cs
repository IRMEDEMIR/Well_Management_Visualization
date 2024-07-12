using Microsoft.EntityFrameworkCore;
using ParsingProjectMVC.Models;

namespace ParsingProjectMVC.Services
{
    public class ParsingDbContext : DbContext
    {
        public ParsingDbContext(DbContextOptions<ParsingDbContext> options) : base(options)
        {
        
        }

        public DbSet<SahaModel> Sahalar { get; set; }  
        public DbSet<KuyuGrubuModel> KuyuGruplari { get; set; }
        public DbSet<KuyuModel> Kuyular { get; set; } 
        public DbSet<WellboreModel> Wellborelar { get; set; }
    }
}
