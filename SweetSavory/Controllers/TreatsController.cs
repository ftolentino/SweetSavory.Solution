using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SweetSavory.Models;

namespace SweetSavory.Controllers
{
  public class TreatsController : Controller
  {
    private readonly SweetSavoryContext _db;

    public TreatsController(SweetSavoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Treat> model = _db.Treats.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
        var thisTreat = _db.Treats
            .Include(treat => treat.JoinEntities)
            .ThenInclude(join => join.Flavor)
            .FirstOrDefault(treat => treat.TreatId == id);
        return View(thisTreat);
    }

    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId ==id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      if (FlavorId !=0)
      {
        _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // public ActionResult Delete(int id)
    // {
    //   var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
    //   return View(thisTag);
    // }

    // [HttpPost, ActionName("Delete")]
    // public ActionResult DeleteConfirmed(int id)
    // {
    //   var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
    //   _db.Tags.Remove(thisTag);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
    // public ActionResult AddRecipe(int id)
    // {
    //   var thisTag = _db.Tags.FirstOrDefault(tag => tag.TagId == id);
    //   ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "Name");
    //   return View(thisTag);
    // }

    // [HttpPost]
    // public ActionResult AddRecipe(Tag tag, int RecipeId)
    // {
    //   if (RecipeId !=0)
    //   {
    //     _db.TagRecipe.Add(new TagRecipe() { RecipeId = RecipeId, TagId = tag.TagId }); 
    //     _db.SaveChanges();
    //   }
    //   return RedirectToAction("Index");
    // }

    // [HttpPost]
    // public ActionResult DeleteRecipe(int joinId)
    // {
    //   var joinEntry = _db.TagRecipe.FirstOrDefault(entry => entry.TagRecipeId == joinId);
    //   _db.TagRecipe.Remove(joinEntry);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }
  }
}