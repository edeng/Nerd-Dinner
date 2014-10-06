using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NerdDinner.Models;


namespace NerdDinner.Models
{
    public class DinnerRepository
    {
        public NerdDinners db = new NerdDinners();

        // 
        // Query Methods

        public IQueryable<Dinner> FindAllDinners()
        {
            return db.Dinners;
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return from dinner in db.Dinners
                where dinner.EventDate > DateTime.Now
                orderby dinner.EventDate
                select dinner;
        }

        public Dinner GetDinner(int id)
        {
            return db.Dinners.SingleOrDefault(d => d.DinnerID == id); 
        }

        // 
        // Insert/Delete Methods

        public void Add(Dinner dinner)
        {
            db.Dinners.Add(dinner);
        }

        public void Delete(Dinner dinner)
        {
            //db.RSVPs.ToList().ForEach(record => db.RSVPs.Remove(record));
            //db.SaveChanges();
            db.RSVPs.RemoveRange(dinner.RSVPs); 
            db.Dinners.Remove(dinner);
        }

        public void ModifyState(Dinner dinner)
        {
            db.Entry(dinner).State = EntityState.Modified; 
        }

        // 
        // Persistence

        public void Save()
        {
            db.SaveChanges();
        }
    }
}