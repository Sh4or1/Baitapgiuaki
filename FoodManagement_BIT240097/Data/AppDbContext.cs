using FoodManagement_BIT240097.Models;
using Microsoft.EntityFrameworkCore;
namespace FoodManagement_BIT240097.Data;
public class AppDbContext : DbContext
{
 public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
 public DbSet<DishCategory_BIT240097> DishCategories => Set<DishCategory_BIT240097>();
 public DbSet<Dish_BIT240097> Dishes => Set<Dish_BIT240097>();
 public DbSet<DishImage_BIT240097> DishImages => Set<DishImage_BIT240097>();
 protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
  var c=modelBuilder.Entity<DishCategory_BIT240097>(); c.ToTable("BIT240097_DishCategories"); c.HasKey(x=>x.Id); c.Property(x=>x.Id).HasColumnName("BIT240097_Id"); c.Property(x=>x.Name).HasColumnName("BIT240097_Name").HasMaxLength(100).IsRequired(); c.Property(x=>x.Description).HasColumnName("BIT240097_Description").HasMaxLength(500); c.HasIndex(x=>x.Name).IsUnique();
  var d=modelBuilder.Entity<Dish_BIT240097>(); d.ToTable("BIT240097_Dishes"); d.HasKey(x=>x.Id); d.Property(x=>x.Id).HasColumnName("BIT240097_Id"); d.Property(x=>x.Name).HasColumnName("BIT240097_Name").HasMaxLength(120).IsRequired(); d.Property(x=>x.Price).HasColumnName("BIT240097_Price").HasColumnType("decimal(18,2)"); d.Property(x=>x.PreparationTime).HasColumnName("BIT240097_PreparationTime"); d.Property(x=>x.IsAvailable).HasColumnName("BIT240097_IsAvailable"); d.Property(x=>x.Description).HasColumnName("BIT240097_Description").HasMaxLength(1000); d.Property(x=>x.DishCategoryId).HasColumnName("BIT240097_DishCategoryId"); d.HasOne(x=>x.Category).WithMany(x=>x.Dishes).HasForeignKey(x=>x.DishCategoryId).OnDelete(DeleteBehavior.Restrict); d.HasIndex(x=>new{x.DishCategoryId,x.Name}).IsUnique();
  var i=modelBuilder.Entity<DishImage_BIT240097>(); i.ToTable("BIT240097_DishImages"); i.HasKey(x=>x.Id); i.Property(x=>x.Id).HasColumnName("BIT240097_Id"); i.Property(x=>x.ImageUrl).HasColumnName("BIT240097_ImageUrl").HasMaxLength(500).IsRequired(); i.Property(x=>x.IsThumbnail).HasColumnName("BIT240097_IsThumbnail"); i.Property(x=>x.DishId).HasColumnName("BIT240097_DishId"); i.HasOne(x=>x.Dish).WithMany(x=>x.Images).HasForeignKey(x=>x.DishId).OnDelete(DeleteBehavior.Cascade);
  // Filtered unique index guarantees at most one thumbnail per dish in SQL Server.
  i.HasIndex(x=>x.DishId).IsUnique().HasFilter("[BIT240097_IsThumbnail] = 1");
 }
}