using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace NerdDinner.Models
{
    public class NerdDinnersInitializer : DropCreateDatabaseAlways<NerdDinners>
    {
        protected override void Seed(NerdDinners context)
        {
            var dinners = new List<Dinner>
            {
                new Dinner
                {
                    Title = "Sample Dinner 1",
                    EventDate = DateTime.Parse("12/31/2015"),
                    Address = "One Microsoft Way",
                    Country = "USA",
                    HostedBy = "scottgu@microsoft.com",
                },

                new Dinner()
                {
                    Title = "Sample Dinner 2",
                    EventDate = DateTime.Parse("05/31/2015"),
                    Address = "Somewhere else",
                    Country = "USA",
                    HostedBy = "scottgu@microsoft.com"
                }
            };

            dinners.ForEach(d => context.Dinners.Add(d));
        }
    }
}