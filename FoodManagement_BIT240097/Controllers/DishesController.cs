using FoodManagement_BIT240097.Data;
using FoodManagement_BIT240097.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace FoodManagement_BIT240097.Controllers;
public class DishesController : Controller
{
 private readonly AppDbContext _db; public DishesController(AppDbContext db)=>_db=db;
 private bool Vi => HttpContext.Session.GetString("lang")=="vi";
 private void Categories(int? selected=null)=>ViewBag.Categories=new SelectList(_db.DishCategories.AsNoTracking().OrderBy(x=>x.Name),"Id","Name",selected);
 public async Task<IActionResult> Index(string? search,int? categoryId,bool? available,decimal? minPrice,decimal? maxPrice,string? sort)
 {
  if(minPrice<0 || maxPrice<0 || (minPrice.HasValue && maxPrice.HasValue && minPrice>maxPrice)) { ViewBag.FilterError=Vi?"Khoảng giá không hợp lệ.":"Invalid price range."; minPrice=null; maxPrice=null; }
  var q=_db.Dishes.AsNoTracking().Include(x=>x.Category).Include(x=>x.Images).AsQueryable();
  if(!string.IsNullOrWhiteSpace(search)) q=q.Where(x=>x.Name.Contains(search)); if(categoryId.HasValue) q=q.Where(x=>x.DishCategoryId==categoryId); if(available.HasValue) q=q.Where(x=>x.IsAvailable==available.Value); if(minPrice.HasValue) q=q.Where(x=>x.Price>=minPrice.Value); if(maxPrice.HasValue) q=q.Where(x=>x.Price<=maxPrice.Value);
  q=sort switch {"price_desc"=>q.OrderByDescending(x=>x.Price),"price_asc"=>q.OrderBy(x=>x.Price),"time_asc"=>q.OrderBy(x=>x.PreparationTime),_=>q.OrderBy(x=>x.Name)};
  ViewBag.Search=search; ViewBag.CategoryId=categoryId; ViewBag.Available=available; ViewBag.MinPrice=minPrice; ViewBag.MaxPrice=maxPrice; ViewBag.Sort=sort; ViewBag.Categories=new SelectList(await _db.DishCategories.AsNoTracking().ToListAsync(),"Id","Name",categoryId); ViewBag.IsVi=Vi;
  return View(await q.ToListAsync());
 }
 public async Task<IActionResult> Details(int? id){if(id==null)return NotFound();var dish=await _db.Dishes.Include(x=>x.Category).Include(x=>x.Images).FirstOrDefaultAsync(x=>x.Id==id);return dish==null?NotFound():View(dish);}
 public async Task<IActionResult> Create(){Categories();return View(new Dish_BIT240097());}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Create(Dish_BIT240097 dish)
 { await ValidateDuplicate(dish); if(!await _db.DishCategories.AnyAsync(x=>x.Id==dish.DishCategoryId))ModelState.AddModelError(nameof(dish.DishCategoryId),Vi?"Loại món không tồn tại.":"Category does not exist."); if(!ModelState.IsValid){Categories(dish.DishCategoryId);return View(dish);} _db.Add(dish);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index)); }
 public async Task<IActionResult> Edit(int? id){if(id==null)return NotFound();var dish=await _db.Dishes.FindAsync(id);if(dish==null)return NotFound();Categories(dish.DishCategoryId);return View(dish);}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id,Dish_BIT240097 dish)
 { if(id!=dish.Id)return BadRequest(); if(!await _db.DishCategories.AnyAsync(x=>x.Id==dish.DishCategoryId))ModelState.AddModelError(nameof(dish.DishCategoryId),Vi?"Loại món không tồn tại.":"Category does not exist."); await ValidateDuplicate(dish); if(!ModelState.IsValid){Categories(dish.DishCategoryId);return View(dish);} _db.Update(dish);try{await _db.SaveChangesAsync();}catch(DbUpdateException){ModelState.AddModelError(nameof(dish.Name),Vi?"Tên món đã tồn tại trong loại này.":"This name already exists in this category.");Categories(dish.DishCategoryId);return View(dish);}return RedirectToAction(nameof(Index)); }
 private async Task ValidateDuplicate(Dish_BIT240097 dish){if(await _db.Dishes.AnyAsync(x=>x.DishCategoryId==dish.DishCategoryId&&x.Name==dish.Name&&x.Id!=dish.Id))ModelState.AddModelError(nameof(dish.Name),Vi?"Tên món bị trùng trong loại này.":"Dish name must be unique within its category.");}
 public async Task<IActionResult> Delete(int? id){if(id==null)return NotFound();var dish=await _db.Dishes.Include(x=>x.Category).FirstOrDefaultAsync(x=>x.Id==id);return dish==null?NotFound():View(dish);}
 [HttpPost,ActionName("Delete"),ValidateAntiForgeryToken] public async Task<IActionResult> DeleteConfirmed(int id){var dish=await _db.Dishes.FindAsync(id);if(dish==null)return NotFound();_db.Dishes.Remove(dish);await _db.SaveChangesAsync();return RedirectToAction(nameof(Index));}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> AddImage(int dishId,string imageUrl,bool isThumbnail){var dish=await _db.Dishes.Include(x=>x.Images).FirstOrDefaultAsync(x=>x.Id==dishId);if(dish==null)return NotFound();if(string.IsNullOrWhiteSpace(imageUrl)){TempData["Error"]="Image URL is required.";return RedirectToAction(nameof(Details),new{id=dishId});} if(isThumbnail)foreach(var old in dish.Images)old.IsThumbnail=false;_db.DishImages.Add(new DishImage_BIT240097{DishId=dishId,ImageUrl=imageUrl.Trim(),IsThumbnail=isThumbnail});await _db.SaveChangesAsync();return RedirectToAction(nameof(Details),new{id=dishId});}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> SetThumbnail(int id){var image=await _db.DishImages.FindAsync(id);if(image==null)return NotFound();var images=await _db.DishImages.Where(x=>x.DishId==image.DishId).ToListAsync();foreach(var item in images)item.IsThumbnail=item.Id==id;await _db.SaveChangesAsync();return RedirectToAction(nameof(Details),new{id=image.DishId});}
 [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> DeleteImage(int id){var image=await _db.DishImages.FindAsync(id);if(image==null)return NotFound();int dishId=image.DishId;_db.DishImages.Remove(image);await _db.SaveChangesAsync();return RedirectToAction(nameof(Details),new{id=dishId});}
 [HttpGet] public IActionResult SetLanguage(string lang,string? returnUrl){HttpContext.Session.SetString("lang",lang=="vi"?"vi":"en");if(!string.IsNullOrEmpty(returnUrl)&&Url.IsLocalUrl(returnUrl))return Redirect(returnUrl);return RedirectToAction(nameof(Index));}
}