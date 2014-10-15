using System.Linq;
using System.Web.Mvc;
using NerdDinner.Models;

public class JsonDinner
{
    public int DinnerID { get; set; }
    public string Title { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string Description { get; set; }
    public int RSVPCount { get; set; }
}

public class SearchController : Controller
{

    DinnerRepository dinnerRepository = new DinnerRepository();

    //
    // AJAX: /Search/SearchByLocation

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult SearchByLocation(decimal longitude, decimal latitude)
    {

        var dinners = dinnerRepository.FindByLocation(latitude, longitude).ToList();

        var jsonDinners = from dinner in dinners
                          select new JsonDinner
                          {
                              DinnerID = dinner.DinnerID,
                              Latitude = dinner.Latitude,
                              Longitude = dinner.Longitude,
                              Title = dinner.Title,
                              Description = dinner.Description,
                              RSVPCount = dinner.RSVPs.Count
                          };

        return Json(jsonDinners.ToList());
    }
}