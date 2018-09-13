using EFCoreTutorialsConsoleApp.DomainModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorialsConsoleApp.DomainModels
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SchoolDB;Trusted_Connection=True;");
        }
    }
}
