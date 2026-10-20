using System.ComponentModel.DataAnnotations;
namespace FoodManagement_BIT220264.Models;
public class DishImage_BIT220264
{
 public int Id { get; set; }
 [Required(ErrorMessage="ImageRequired")][StringLength(500)] public string ImageUrl { get; set; } = "";
 public bool IsThumbnail { get; set; }
 [Range(1, int.MaxValue, ErrorMessage="DishRequired")] public int DishId { get; set; }
 public Dish_BIT220264? Dish { get; set; }
}