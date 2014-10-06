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
        DinnerRepository dinnerRepository = new DinnerRepository();


        //
        // GET: /
        public ActionResult Index()
        {
            return View(dinnerRepository.FindUpcomingDinners());
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
                UpdateModel(dinner);
                dinnerRepository.Add(dinner);
                dinnerRepository.Save();

                return RedirectToAction("Index");
            }

            return View(dinner);
        }
    }
}