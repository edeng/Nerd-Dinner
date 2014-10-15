using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using NerdDinner.Models;

namespace NerdDinner.Tests.Fakes
{
    class FakeDinnerRepository : IDinnerRepository
    {
        private List<Dinner> dinnerList;

        public FakeDinnerRepository(List<Dinner> dinners)
        {
            dinnerList = dinners; 
        }

        public IQueryable<NerdDinner.Models.Dinner> FindAllDinners()
        {
            return dinnerList.AsQueryable(); 
        }

        public IQueryable<NerdDinner.Models.Dinner> FindUpcomingDinners()
        {
            return (from dinner in dinnerList
                where dinner.EventDate > DateTime.Now
                select dinner).AsQueryable(); 
        }

        public NerdDinner.Models.Dinner GetDinner(int id)
        {
            return dinnerList.SingleOrDefault(d => d.DinnerID == id);
        }

        public void Add(NerdDinner.Models.Dinner dinner)
        {
            dinnerList.Add(dinner); 
        }

        public void Delete(NerdDinner.Models.Dinner dinner)
        {
            dinnerList.Remove(dinner);
        }

        public IQueryable<NerdDinner.Models.Dinner> FindByLocation(decimal latitude, decimal longitude)
        {
            return (from dinner in dinnerList
                where dinner.Latitude == latitude && dinner.Longitude == longitude
                select dinner).AsQueryable();
        }

        public void Save()
        {
        }

        public void ModifyState(Dinner dinner)
        {
        }
    }
}
