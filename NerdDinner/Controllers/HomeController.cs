using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class HomeController : Controller
    {
        NerdDinners nerdDinners = new NerdDinners();

        //
        // GET: /
        public ActionResult Index()
        {
            var dinners = from d in nerdDinners.Dinners
                          where d.EventDate > DateTime.Now
                          select d;
            return View(dinners.ToList());
        }

        //
        // GET: /Home/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // GET: /Home/Create
        [HttpPost]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                nerdDinners.Dinners.Add(dinner);
                nerdDinners.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(dinner);
        }
    }
}