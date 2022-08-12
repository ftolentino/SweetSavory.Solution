using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
namespace SweetSavory.Controllers
{
  [Authorize]
  public class FlavorsController : Controller
  {
    private readonly SweetSavoryContext _db;
    private readonly UserManager<ApplicationUser> _userManager; 

    public FlavorsController(UserManager<ApplicationUser> userManager, SweetSavoryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        var userFlavors = _db.Flavors.Where(entry => entry.User.Id == currentUser.Id).ToList();
        return View(userFlavors);
    }

    public ActionResult Create()
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Flavor flavor, int TreatId, string Name)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      flavor.User = currentUser;
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      if (TreatId != 0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = flavor.FlavorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // public ActionResult Details(int id)
    // {
    //   var thisRecipe = _db.Recipes
    //       .Include(recipe => recipe.JoinEntities)
    //       .ThenInclude(join => join.Tag)
    //       .FirstOrDefault(recipe => recipe.RecipeId == id);
    //   return View(thisRecipe);
    // }
    
    // public ActionResult Edit(int id)
    // {
    //   var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
    //   ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Name");
    //   return View(thisRecipe);
    // }

    // [HttpPost]
    // public ActionResult Edit(Recipe recipe, int TagId)
    // {
    //   if (TagId != 0)
    //   {
    //     _db.TagRecipe.Add(new TagRecipe() { TagId = TagId, RecipeId = recipe.RecipeId });
    //   }
    //   _db.Entry(recipe).State = EntityState.Modified;
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    // public ActionResult Delete(int id)
    // {
    //   var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
    //   return View(thisRecipe);
    // }

    // [HttpPost, ActionName("Delete")]
    // public ActionResult DeleteConfirmed(int id)
    // {
    //   var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
    //   _db.Recipes.Remove(thisRecipe);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    // public ActionResult AddTag(int id)
    // {
    //   var thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
    //   ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Name");
    //   return View(thisRecipe);
    // }

    // [HttpPost]
    // public ActionResult AddTag(Recipe recipe, int TagId)
    // {
    //   if (TagId !=0)
    //   {
    //     _db.TagRecipe.Add(new TagRecipe() { TagId = TagId, RecipeId = recipe.RecipeId }); 
    //     _db.SaveChanges();
    //   }
    //   return RedirectToAction("Index");
    // }

    // [HttpPost]
    // public ActionResult DeleteTag(int joinId)
    // {
    //   var joinEntry = _db.TagRecipe.FirstOrDefault(entry => entry.TagRecipeId == joinId);
    //   _db.TagRecipe.Remove(joinEntry);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
    // [HttpGet]
    // public ActionResult ShowSearch()
    // {
    //   return View();
    // }

    // [HttpPost]
    // public ActionResult ShowSearch(string searchPhrase)
    // {
    //     List<Recipe> model = _db.Recipes.Where(p => p.Name.ToLower().Contains(searchPhrase.ToLower()) || p.Tag.Name.ToLower().Contains(searchPhrase.ToLower())).ToList(); 
    //     return View("Index", model);
    // }
  }
}