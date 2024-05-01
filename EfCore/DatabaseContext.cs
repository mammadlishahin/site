using Microsoft.EntityFrameworkCore;
using Models;

namespace EfCore;


public class DatabaseContext : DbContext 
{
    public DbSet<User> Users { get; set; }
    public DbSet<Terapiler> Terapiler { get; set; }
    public DbSet<Soru> Sorular { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySQL("Server=localhost;Database=new_databae;UserID=mydbuser;Password=**********;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(x => x.VerificationCode)
            .IsRequired(false); // Boş değerleri kabul etmek için IsRequired(false) kullanılır

        modelBuilder.Entity<Terapiler>()
            .Property(x => x.IstegeBagli)
            .IsRequired(false); // Boş değerleri kabul etmek için IsRequired(false) kullanılır

        modelBuilder.Entity<Terapiler>()
            .Property(x => x.Url)
            .IsRequired(false); // Boş değerleri kabul etmek için IsRequired(false) kullanılır

        modelBuilder.Entity<Terapiler>()
            .Property(x => x.StartDateTime)
            .HasColumnType("datetime"); // saniye ve milisaniye bileşenlerini saklamak için datetime sütun türünü kullan

        modelBuilder.Entity<Terapiler>()
            .Property(x => x.CreationDateTime)
            .HasColumnType("datetime"); // saniye ve milisaniye bileşenlerini saklamak için datetime sütun türünü kullan

         modelBuilder.Entity<Soru>()
            .Property(x => x.User)
            .IsRequired(false); // Boş değerleri kabul etmek için IsRequired(false) kullanılır
        modelBuilder.Entity<Soru>()
            .Property(x => x.Date)
            .HasColumnType("datetime"); // saniye ve milisaniye bileşenlerini saklamak için datetime sütun türünü kullan


    }
    
}

