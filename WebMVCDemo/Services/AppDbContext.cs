using Microsoft.EntityFrameworkCore;
using WebMVCDemo.Models;

namespace WebMVCDemo.Services
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .Build();

            System.Console.WriteLine(config["Logging:LogLevel:Default"]);
            // config.GetConnectionString("Default");
            optionsBuilder.UseMySQL(config["ConnectionStrings:Default"])
            //.UseLazyLoadingProxies()
            .LogTo(System.Console.WriteLine, LogLevel.Information);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresess { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new AddressEntityTypeConfiguration().Configure(modelBuilder.Entity<Address>());

            modelBuilder.Entity<Employee>()
            .Property(e => e.Department)
            .HasColumnType("varchar(255)")
            .HasDefaultValue("Information Technology");

            modelBuilder.Entity<EmployeePhoneNumber>()
            .HasKey(epn => new { epn.EmployeeId, epn.PhoneNumber });

        }
    }
}
