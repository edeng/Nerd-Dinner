using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        private DinnerRepository dinnerRepository = new DinnerRepository();

        // GET: /Dinners/
        public ActionResult Index()
        {
            var dinners = dinnerRepository.FindUpcomingDinners().ToList();

            return View(dinners);
        }

        // GET: /Dinners/Details/5
        public ActionResult Details(int id)
        {

            Dinner dinner = dinnerRepository.GetDinner(id);


            if (dinner == null)
            {
                return HttpNotFound();
            }

            return View(dinner);
        }
    }
}
