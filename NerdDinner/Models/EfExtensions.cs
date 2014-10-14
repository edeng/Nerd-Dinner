using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public static class EfExtensions
    {
        [EdmFunction("dbo", "DistanceBewteen")]
        public static decimal DistanceBetween(decimal Lat1, decimal Long1, decimal Lat2, decimal Long2)
        {
            throw new NotImplementedException();
        }

       
    }
}