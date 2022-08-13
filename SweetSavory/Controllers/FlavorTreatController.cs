using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SweetSavory.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SweetSavory.Controllers
{
  public class FlavorTreatController : Controller
  {
    private readonly SweetSavoryContext _db;

    public FlavorTreatController(SweetSavoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      ViewBag.PageTitle = "View All Treats and Flavors";
      List<FlavorTreat> model = _db.FlavorTreat.ToList();
      return View(model);
    }

    [HttpPost]
    public ActionResult Create(FlavorTreat flavortreat)
    {
      if (_db.FlavorTreat.FirstOrDefault(
              ft => ft.FlavorId == flavortreat.FlavorId && 
                    ft.TreatId == flavortreat.TreatId) == null)
      {
        _db.FlavorTreat.Add(flavortreat);
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
  }
}

