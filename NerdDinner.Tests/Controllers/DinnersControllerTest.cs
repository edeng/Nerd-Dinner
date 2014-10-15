using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using Moq;
using NerdDinner.Controllers;
using NerdDinner.Models;
using NerdDinner.Tests.Fakes;
using NUnit.Framework;
using System.Web.Mvc;


namespace NerdDinner.Tests.Controllers
{
    [TestFixture]
    class DinnersControllerTest
    {
        private List<Dinner> CreateTestDinners()
        {
           List<Dinner> dinners = new List<Dinner>();

            for (int i = 0; i < 101; i++)
            {
                Dinner sampleDinner = new Dinner()
                {
                    DinnerID = i, 
                    Title = "Sample Dinner", 
                    HostedBy = "SomeUser", 
                    Address = "Some Address", 
                    Country = "USA", 
                    ContactPhone = "425-555-1212",
                    Description = "Some description", 
                    EventDate = DateTime.Now.AddDays(i),
                    Latitude = 99, 
                    Longitude = -99
                };
                dinners.Add(sampleDinner);
            }
            return dinners;
        }

        DinnersController CreateDinnersController()
        {
            var repository = new FakeDinnerRepository(CreateTestDinners()); 
            return new DinnersController(repository);
        }

        [TestCase]
        public void DetailsActionShouldReturnNotFoundViewForBogusDinner()
        {
            // Arrange
            var controller = CreateDinnersController();

            // Act
            var result = controller.Details(999) as ViewResult; 

            // Assert
            Assert.AreEqual("NotFound", result.ViewName); 
        }

        [TestCase]
        public void DetailsActionShouldReturnViewForExistingDinner()
        {
            // Arrange 
            var controller = CreateDinnersController();

            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult), result);
        }

        [TestCase]
        public void EditActionShouldReturnViewForValidDinner()
        {
            // Arrange
            var controller = CreateDinnersControllerAs("SomeUser");

            Dinner sampleDinner = new Dinner()
            {
                DinnerID = 1,
                Title = "Sample Dinner",
                HostedBy = "SomeUser",
                Address = "Some Address",
                Country = "USA",
                ContactPhone = "425-555-1212",
                Description = "Some description",
                EventDate = DateTime.Now.AddDays(1),
                Latitude = 99,
                Longitude = -99
            };

            // Act
            var result = controller.Edit(sampleDinner);

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult), result);
        }

        [TestCase]
        public void EditActionShouldReturnEditViewWhenValidOwner()
        {
            // Arange
            var controller = CreateDinnersControllerAs("SomeUser"); 

            // Act
            var result = controller.Edit(1); 

            // Assert
            Assert.IsInstanceOf(typeof(ActionResult), result);
        }

        [TestCase]
        public void EditActionShouldReturnInvalidOwnerViewWhenInvalidOwner()
        {
            // Arrange
            var controller = CreateDinnersControllerAs("NotOwnerUser"); 

            // Act
            var result = controller.Edit(1) as ViewResult; 

            // Assert
            Assert.AreEqual("InvalidOwner", result.ViewName); 
        }

        [TestCase]
        public void EditActionShouldRedirectWhenUpdateSuccessful()
        {
            // Arrange
            var controller = CreateDinnersControllerAs("SomeUser");

            var formValues = new FormCollection()
            {
                {"Title", "Another value"},
                {"Description", "Another description"}
            };

            controller.ValueProvider = formValues.ToValueProvider();

            Dinner dinner = new Dinner()
            {
                HostedBy = "SomeUser"
            };

            // Act
            var result = (RedirectToRouteResult)controller.Edit(dinner);

            // Assert 
            Assert.AreEqual("Details", result.RouteValues["Action"]);
        }

        [TestCase]
        public void EditActionShouldRedisplayWithErrorsWhenUpdateFails()
        {
            // Arrange 
            var controller = CreateDinnersControllerAs("SomeUser");

            var formValues = new FormCollection()
            {
                {"EventDate", ""}
            };

            controller.ValueProvider = formValues.ToValueProvider();

            // Act
            var result = (ViewResult)controller.Edit(1); 

            // Assert 
            Assert.IsNotNull(result, "Expected redisplay f view");
        }

        DinnersController CreateDinnersControllerAs(string userName)
        {
            var mock = new Mock<ControllerContext>();
            mock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userName);
            mock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            var controller = CreateDinnersController();
            controller.ControllerContext = mock.Object;

            return controller; 
        }

    }
}
   