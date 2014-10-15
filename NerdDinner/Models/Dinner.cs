using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Linq;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public class Dinner
    {
        public Dinner()
        {
            this.RSVPs = new List<RSVP>();
        }

        public int DinnerID { get; set; }

        [Required(ErrorMessage = "Please enter a Dinner Title")]
        [StringLength(20, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the Date of the Dinner")]
        [DisplayName("Event Date")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Please enter the location of the Dinner")]
        [StringLength(30, ErrorMessage = "Address is too long")]
        public string Address { get; set; }

        //[Required(ErrorMessage = "Please enter your email address")]
        //[RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address")]
        //[DisplayName("Hosted By")]
        public string HostedBy { get; set; }
        public string Country { get; set; }

        public string Description { get; set; }

        [DisplayName("Contact Phone")]
        public string ContactPhone { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public virtual ICollection<RSVP> RSVPs { get; set; }

        public bool IsHostedBy(string userName)
        {
            return HostedBy.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsUserRegistered(string userName)
        {
            return RSVPs.Any(r => r.AttendeeEmail.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool IsValid
        {
            get { return (GetRuleViolations() != null); }
        }

        public RuleViolation GetRuleViolations()
        {
            if (String.IsNullOrEmpty(EventDate.ToString()))
                return new RuleViolation("Field value text is required", "SomeField");
            else
                return null;

        }

        public void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new ApplicationException("Rule violations prevent saving");
        }
    }
}