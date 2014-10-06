using System;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class DinnersController : Controller
    {
        NerdDinners nerdDinners = new NerdDinners();


        private DinnerRepository dinnerRepository = new DinnerRepository();


        // GET: /Dinners/
        //      /Dinners?page=2
        public ActionResult Index(int? page)
        {
            const int pageSize = 10;

            var dinners = dinnerRepository.FindUpcomingDinners().ToList();
            var paginatedDinners = new PaginatedList<Dinner>(dinners, page ?? 0, pageSize);

           // var paginatedDinners = dinners.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();

            return View(paginatedDinners);
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

        // 
        // GET: /Dinners/Edit/2
        [Authorize]
        public ActionResult Edit(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            ViewData["Countries"] = new SelectList(PhoneValidator.Countries, dinner.Country);

            return View(dinner); 
        }

        // 
        // POST: /Dinners/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditVerify(Dinner modifiedDinner)
        {
            if (ModelState.IsValid)
            {
                dinnerRepository.ModifyState(modifiedDinner);
                dinnerRepository.Save();
                return RedirectToAction("Details", new { id = modifiedDinner.DinnerID });
            }

            return View(modifiedDinner);
        }

        // 
        // GET: /Dinners/Create

        public ActionResult Create()
        {

            Dinner dinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7)
            };

            ViewData["Countries"] = new SelectList(PhoneValidator.Countries, dinner.Country);

            return View(dinner); 
        }

        // 
        // POST: /Dinners/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                UpdateModel(dinner); 

                dinnerRepository.Add(dinner); 
                dinnerRepository.Save();

                return RedirectToAction("Details", new {id = dinner.DinnerID});
            }

            ViewData["Countries"] = new SelectList(PhoneValidator.Countries, dinner.Country);

            return View(dinner); 
        }

        public ActionResult Delete(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");
            else
                return View(dinner);
        }


        //
        // HTTP POST: /Dinners/Delete/1

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id, string confirmButton)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            dinnerRepository.Delete(dinner);
            dinnerRepository.Save();

            return View("Deleted"); 
        }



    }
}
