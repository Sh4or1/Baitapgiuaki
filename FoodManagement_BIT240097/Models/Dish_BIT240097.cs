using System.ComponentModel.DataAnnotations;
namespace FoodManagement_BIT240097.Models;
public class Dish_BIT240097
{
 public int Id { get; set; }
 [Required(ErrorMessage="NameRequired")][StringLength(120)] public string Name { get; set; } = "";
 [Range(typeof(decimal), "0.01", "999999999", ErrorMessage="PricePositive")] public decimal Price { get; set; }
 [Range(1, 1440, ErrorMessage="TimePositive")] public int PreparationTime { get; set; }
 public bool IsAvailable { get; set; } = true;
 [StringLength(1000)] public string? Description { get; set; }
 [Range(1, int.MaxValue, ErrorMessage="CategoryRequired")] public int DishCategoryId { get; set; }
 public DishCategory_BIT240097? Category { get; set; }
 public ICollection<DishImage_BIT240097> Images { get; set; } = new List<DishImage_BIT240097>();
 public string TimeClass => PreparationTime <= 15 ? "Quick / Nhanh" : "Regular / Thông thường";
}