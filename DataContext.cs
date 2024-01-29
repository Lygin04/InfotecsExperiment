using InfotecsExperiment.Entity;
using Microsoft.EntityFrameworkCore;
using File = InfotecsExperiment.Entity.File;

namespace InfotecsExperiment
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<File?> Files { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Value> Values { get; set; }
    }
}
