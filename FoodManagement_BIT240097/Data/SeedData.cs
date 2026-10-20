using FoodManagement_BIT240097.Models;
namespace FoodManagement_BIT240097.Data;
public static class SeedData
{
 public static void Initialize(AppDbContext db)
 {
  if (!db.DishCategories.Any()) { db.DishCategories.AddRange(new DishCategory_BIT240097{Name="Main course",Description="Main dishes"},new DishCategory_BIT240097{Name="Drinks",Description="Hot and cold drinks"},new DishCategory_BIT240097{Name="Desserts",Description="Sweet dishes"}); db.SaveChanges(); }
  if (!db.Dishes.Any()) { var cats=db.DishCategories.OrderBy(x=>x.Id).ToList(); db.Dishes.AddRange(new Dish_BIT240097{Name="Grilled chicken rice",Price=55000,PreparationTime=20,IsAvailable=true,Description="Grilled chicken served with rice.",DishCategoryId=cats[0].Id},new Dish_BIT240097{Name="Beef pho",Price=60000,PreparationTime=12,IsAvailable=true,Description="Vietnamese beef noodle soup.",DishCategoryId=cats[0].Id},new Dish_BIT240097{Name="Fried rice",Price=40000,PreparationTime=15,IsAvailable=true,Description="Fried rice with vegetables.",DishCategoryId=cats[0].Id},new Dish_BIT240097{Name="Lemon tea",Price=20000,PreparationTime=5,IsAvailable=true,Description="Refreshing lemon tea.",DishCategoryId=cats[1].Id},new Dish_BIT240097{Name="Flan cake",Price=25000,PreparationTime=10,IsAvailable=false,Description="Caramel custard dessert.",DishCategoryId=cats[2].Id}); db.SaveChanges(); }
 }
}