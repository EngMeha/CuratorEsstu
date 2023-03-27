using DataLayer.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer
{
    public class DiplomContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventOfStudent> EventOfStudent { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<BasisOfLearning> BasisOfLearning { get; set; }
        public DbSet<HistoryChangeStudent> HistoryChangeStudent { get; set; }
        public DbSet<CraduationDepartament> CraduationDepartament { get; set; }
        public DbSet<FormOfStudy> FormOfStudy { get; set; }


        public DiplomContext(DbContextOptions<DiplomContext> options) : base(options) { }
    }

    public class Factory: IDesignTimeDbContextFactory<DiplomContext>
    {
        public DiplomContext CreateDbContext(string[] args)
        {
            var optionsBuider = new DbContextOptionsBuilder<DiplomContext>();
            optionsBuider.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=diplom;TrustServerCertificate=True;", b => b.MigrationsAssembly("DataLayer"));
            return new DiplomContext(optionsBuider.Options);
        }
    }
}