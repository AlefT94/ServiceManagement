using ServiceManagement.Domain.Entities;

namespace ServiceManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employees { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.CompanyName).IsRequired().HasMaxLength(200);
            entity.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(15);
            entity.OwnsOne(c => c.Address);
            entity.HasOne(c => c.User)
                .WithOne(u => u.Company)
                .HasForeignKey<Company>(u => u.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
            entity.HasOne(c => c.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(u => u.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(c => c.CompanyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            // Unique index for Email
            entity.HasIndex(u => u.Email).IsUnique();
        });


        base.OnModelCreating(modelBuilder);

        // setting tha tables name to lower case
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
            {
                entity.SetTableName(tableName.ToLower());
            }
        }
        
    }
}
