using Microsoft.AspNetCore.Mvc;
using SweetNSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Authorization;  //"Allows us to actually authorize users" (Lsn 9).
using Microsoft.AspNetCore.Identity;  //"[Allows] our controller [to] interact with users from the database" (Lsn 9). 
using System.Threading.Tasks;  //"[Is] necessary to call async methods" (Lsn 9). 
using System.Security.Claims;  /* "[I]mportant for using claim based authorization. A claim is a form of user identification. It states who a user is, not what the user can actually do. While the user identification itself doesn't authorize a user to do anything, it is necessary to first identify a user (through a claim) in order to determine whether they should be authorized" (Lsn 9). */

namespace SweetNSavory.Controllers
{
  //[Authorize]  //This attribute allows access to the 'TreatsController' only if a user is logged in.  
  public class TreatsController : Controller
  {
    private readonly SweetNSavoryContext _db;
    private readonly UserManager<ApplicationUser> _userManager; 

    public TreatsController(UserManager<ApplicationUser> userManager, SweetNSavoryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

      public ActionResult Index()
      {
        //return View(_db.Treats.ToList().OrderBy(model => model.Rating).ToList());
        return View(_db.Treats.ToList()); 
      }

/*
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userTreats = _db.Treats.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userTreats);
    } */

    [Authorize] 
    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      treat.User = currentUser;
      _db.Treats.Add(treat);
      _db.SaveChanges();
      if (FlavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
        .Include(treat => treat.JoinTreFlav)
        .ThenInclude(join => join.Flavor)
        //.Include(treat => treat.JoinTreIng)
        //.ThenInclude(join => join.Ingredient)
        .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [Authorize] 
    public ActionResult Edit(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize] 
    public ActionResult AddFlavor(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      foreach(TreatFlavor entry in _db.TreatFlavor)  //From Joe's MachineController file from the 10/14/22 CR. -SM
        {
          if(treat.TreatId == entry.TreatId && FlavorId == entry.FlavorId)
          {
            return RedirectToAction("Index");
          }
        }
      if (FlavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [Authorize] 
    public ActionResult AddIngredient(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.IngredientId = new SelectList(_db.Ingredients, "IngredientId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddIngredient(Treat treat, int IngredientId)
    {
      foreach(TreatIngredient entry in _db.TreatIngredient)
        {  //Blocks the Ingredient from being added if this given element matches what the user is trying to add. (Prevents duplicate entries of an Ingredient.)
          if((IngredientId == entry.IngredientId) && (treat.TreatId == entry.TreatId))
          {
            return RedirectToAction("Index");
          }
        }
      if (IngredientId != 0)
      {
        _db.TreatIngredient.Add(new TreatIngredient() { IngredientId = IngredientId, TreatId = treat.TreatId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [Authorize] 
    public ActionResult Delete(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize] 
    [HttpPost]
    public ActionResult DeleteFlavor(int joinId)
    {
      var joinEntry = _db.TreatFlavor.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      _db.TreatFlavor.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize] 
    [HttpPost]
    public ActionResult DeleteIngredient(int joinId)
    {
      var joinEntry = _db.TreatIngredient.FirstOrDefault(entry => entry.TreatIngredientId == joinId);
      _db.TreatIngredient.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
