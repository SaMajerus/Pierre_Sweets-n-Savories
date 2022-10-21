// using Microsoft.AspNetCore.Mvc;
// using SweetNSavory.Models;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;

// namespace SweetNSavory.Controllers
// {
//   public class OrdersController : Controller
//   {
//     private readonly SweetNSavoryContext _db;

//     public OrdersController(SweetNSavoryContext db)
//     {
//       _db = db;
//     }

//     public ActionResult Index()
//     {
//       List<Order> model = _db.Orders.ToList();
//       return View(model);
//     }

//     public ActionResult Create()
//     {
//       return View();
//     }

//     [HttpPost]
//     public ActionResult Create(Order ingredient)
//     {
//       _db.Orders.Add(ingredient);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult Details(int id)
//     {
//       var thisOrder = _db.Orders
//         .Include(ingredient => ingredient.JoinRecIng)
//         .ThenInclude(join => join.Recipe)
//         .FirstOrDefault(ingredient => ingredient.OrderId == id);
//       return View(thisOrder);
//     }

//     public ActionResult Edit(int id)
//     {
//       Order thisOrder = _db.Orders.FirstOrDefault(ingredient => ingredient.OrderId == id);
//       return View(thisOrder);
//     }

//     [HttpPost]
//     public ActionResult Edit(Order ingredient)
//     {
//       _db.Entry(ingredient).State = EntityState.Modified;
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }

//     public ActionResult Delete(int id)
//     {
//       Order thisOrder = _db.Orders.FirstOrDefault(ingredient => ingredient.OrderId == id);
//       return View(thisOrder);
//     }

//     [HttpPost, ActionName("Delete")]
//     public ActionResult DeleteConfirmed(int id)
//     {
//       Order thisOrder = _db.Orders.FirstOrDefault(ingredient => ingredient.OrderId == id);
//       _db.Orders.Remove(thisOrder);
//       _db.SaveChanges();
//       return RedirectToAction("Index");
//     }
//   }
// }