using System.ComponentModel.DataAnnotations;
namespace FoodManagement_BIT240097.Models;
public class DishCategory_BIT240097
{
 public int Id { get; set; }
 [Required(ErrorMessage="NameRequired")][StringLength(100)] public string Name { get; set; } = "";
 [StringLength(500)] public string? Description { get; set; }
 public ICollection<Dish_BIT240097> Dishes { get; set; } = new List<Dish_BIT240097>();
}