using FizzWare.NBuilder;
using NerdDinner.Models;
using NUnit.Framework;

namespace NerdDinner.Tests.Models
{
    [TestFixture]
    public class DinnerTest
    {
        [TestCase("Michael", "Michael", true)]
        [TestCase("Michael", "Eden", false)]
        public void IsHostedByReturnsCorrectValues(string host, string expectedHost, bool expectedValue)
        {
            //Arrange
            var dinner = Builder<Dinner>
                .CreateNew()
                .Do(d => d.HostedBy = host)
                .Build();


            //Act
            bool isHosted = dinner.IsHostedBy(expectedHost);

            //Assert
            Assert.That(isHosted, Is.EqualTo(expectedValue));
        }

        [TestCase("Michael", "Michael", true)]
        [TestCase("Michael", "Eden", false)]
        public void IsUserRegisteredReturnsCorrectValues(string user, string testValue, bool expectedValue)
        {
            //Arrange
            var rsvps = Builder<RSVP>
                .CreateListOfSize(10)
                .TheLast(1)
                .Do(r => r.AttendeeEmail = user)
                .Build();

            var dinner = Builder<Dinner>
                .CreateNew()
                .Do(d => d.RSVPs = rsvps)
                .Build();

            //Act
            bool result = dinner.IsUserRegistered(testValue); 

            //Assert
            Assert.That(result, Is.EqualTo(expectedValue));
        }
    }
}
