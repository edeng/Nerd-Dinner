using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Web;
using System.Data.Entity;
using NerdDinner.Models;


namespace NerdDinner.Models
{
    public class DinnerRepository : IDinnerRepository 
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

        public IQueryable<Dinner> FindByLocation(decimal latitude, decimal longitude)
        {

            //var dinners = from dinner in FindUpcomingDinners()
            //              join i in NearestDinners(latitude, longitude)
            //              on dinner.DinnerID equals i.DinnerID
            //              select dinner;

            var dinners = FindUpcomingDinners().ToList();
            var nearestDinners = NearestDinners(latitude, longitude);
            var nearestDinnerIds = nearestDinners.Select(d => d.DinnerID).ToList();
            return dinners.Where(d => nearestDinnerIds.Contains(d.DinnerID)).AsQueryable();

        }

        public IQueryable<Dinner> NearestDinners(decimal lat1, decimal long1)
        {
            //var dinners = from dinner in FindUpcomingDinners()
            //    where DistanceBetween(lat1, long1, dinner.Latitude, dinner.Longitude) < 100
            //    select dinner;

            var dinners = FindUpcomingDinners().ToList();
            dinners.Where(d => DistanceBetween(lat1, long1, d.Latitude, d.Longitude) < 100).ToList();

            return dinners.AsQueryable(); 
        }

        public decimal DistanceBetween(decimal lat1, decimal long1, decimal lat2, decimal long2)
        {
            decimal dLat1InRad = lat1 * (decimal)(Math.PI/180.0);
            decimal dLong1InRad = long1 * (decimal)(Math.PI / 180.0);
            decimal dLat2InRad = lat2 * (decimal)(Math.PI / 180.0);
            decimal dLong2InRad = long2 * (decimal)(Math.PI / 180.0);

            decimal dLongitude = dLong2InRad - dLong1InRad;
            decimal dLatitude = dLat2InRad - dLat1InRad; 

            // intermediate result a
            double a = Math.Pow(((Math.Sin((double)dLatitude / 2.0)) + Math.Cos((double)dLat1InRad)
                        * Math.Cos((double)dLat2InRad)
                        * Math.Pow(Math.Sin((double)dLongitude / 2.0), 2)), 2);

            // intermediate result c (great circle distance in radians)
            double c = 2.0*Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));
            decimal kEarthRadius = (decimal)6376.5;

            decimal dDistance = kEarthRadius * (decimal)c;

            return dDistance; 
        }

        // 
        // Insert/Delete Methods

        public void Add(Dinner dinner)
        {
            db.Dinners.Add(dinner);
        }

        public void Delete(Dinner dinner)
        {
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