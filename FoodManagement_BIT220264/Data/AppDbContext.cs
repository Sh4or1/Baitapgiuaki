using FoodManagement_BIT220264.Models;
using Microsoft.EntityFrameworkCore;
namespace FoodManagement_BIT220264.Data;
public class AppDbContext : DbContext
{
 public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
 public DbSet<DishCategory_BIT220264> DishCategories => Set<DishCategory_BIT220264>();
 public DbSet<Dish_BIT220264> Dishes => Set<Dish_BIT220264>();
 public DbSet<DishImage_BIT220264> DishImages => Set<DishImage_BIT220264>();
 protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
  var c=modelBuilder.Entity<DishCategory_BIT220264>(); c.ToTable("BIT220264_DishCategories"); c.HasKey(x=>x.Id); c.Property(x=>x.Id).HasColumnName("BIT220264_Id"); c.Property(x=>x.Name).HasColumnName("BIT220264_Name").HasMaxLength(100).IsRequired(); c.Property(x=>x.Description).HasColumnName("BIT220264_Description").HasMaxLength(500); c.HasIndex(x=>x.Name).IsUnique();
  var d=modelBuilder.Entity<Dish_BIT220264>(); d.ToTable("BIT220264_Dishes"); d.HasKey(x=>x.Id); d.Property(x=>x.Id).HasColumnName("BIT220264_Id"); d.Property(x=>x.Name).HasColumnName("BIT220264_Name").HasMaxLength(120).IsRequired(); d.Property(x=>x.Price).HasColumnName("BIT220264_Price").HasColumnType("decimal(18,2)"); d.Property(x=>x.PreparationTime).HasColumnName("BIT220264_PreparationTime"); d.Property(x=>x.IsAvailable).HasColumnName("BIT220264_IsAvailable"); d.Property(x=>x.Description).HasColumnName("BIT220264_Description").HasMaxLength(1000); d.Property(x=>x.DishCategoryId).HasColumnName("BIT220264_DishCategoryId"); d.HasOne(x=>x.Category).WithMany(x=>x.Dishes).HasForeignKey(x=>x.DishCategoryId).OnDelete(DeleteBehavior.Restrict); d.HasIndex(x=>new{x.DishCategoryId,x.Name}).IsUnique();
  var i=modelBuilder.Entity<DishImage_BIT220264>(); i.ToTable("BIT220264_DishImages"); i.HasKey(x=>x.Id); i.Property(x=>x.Id).HasColumnName("BIT220264_Id"); i.Property(x=>x.ImageUrl).HasColumnName("BIT220264_ImageUrl").HasMaxLength(500).IsRequired(); i.Property(x=>x.IsThumbnail).HasColumnName("BIT220264_IsThumbnail"); i.Property(x=>x.DishId).HasColumnName("BIT220264_DishId"); i.HasOne(x=>x.Dish).WithMany(x=>x.Images).HasForeignKey(x=>x.DishId).OnDelete(DeleteBehavior.Cascade);
  // Filtered unique index guarantees at most one thumbnail per dish in SQL Server.
  i.HasIndex(x=>x.DishId).IsUnique().HasFilter("[BIT220264_IsThumbnail] = 1");
 }
}