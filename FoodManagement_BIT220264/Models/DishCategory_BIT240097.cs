using System.ComponentModel.DataAnnotations;
namespace FoodManagement_BIT220264.Models;
public class DishCategory_BIT220264
{
 public int Id { get; set; }
 [Required(ErrorMessage="NameRequired")][StringLength(100)] public string Name { get; set; } = "";
 [StringLength(500)] public string? Description { get; set; }
 public ICollection<Dish_BIT220264> Dishes { get; set; } = new List<Dish_BIT220264>();
}