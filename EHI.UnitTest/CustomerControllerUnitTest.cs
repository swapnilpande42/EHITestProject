using EHI.Domain;
using EHI.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace EHI.UnitTest
{
    public class CustomerControllerUnitTest
    {
        [Fact]
        public async Task Index_ReturnsViewResult_WithCustomerList()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.GetAllActiveCustomers().Result).Returns(GetAllMockCustomers());

            var controller = new CustomersController(mock.Object);

            //Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Customer>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithCustomer()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.GetCustomer(1).Result).Returns(GetAllMockCustomers().First()).Verifiable();

            var controller = new CustomersController(mock.Object);

            //Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ViewResult>(viewResult);
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithoutCustomer()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.GetCustomer(3).Result).Returns(GetAllMockCustomers().First()).Verifiable();

            var controller = new CustomersController(mock.Object);

            //Act
            var result = await controller.Details(1);

            // Assert
            var notFound = Assert.IsType<NotFoundResult>(result);
            Assert.IsType<NotFoundResult>(notFound);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithCustomer()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.GetCustomer(1).Result).Returns(GetAllMockCustomers().First()).Verifiable();

            var controller = new CustomersController(mock.Object);

            //Act
            var result = await controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ViewResult>(viewResult);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithoutCustomer()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.GetCustomer(3).Result).Returns(GetAllMockCustomers().First()).Verifiable();

            var controller = new CustomersController(mock.Object);

            //Act
            var result = await controller.Edit(1);

            // Assert
            var notFound = Assert.IsType<NotFoundResult>(result);
            Assert.IsType<NotFoundResult>(notFound);
        }

        [Fact]
        public async Task Create_ReturnsViewResult_WhenModelStateIsInValid()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.GetAllActiveCustomers().Result).Returns(GetAllMockCustomers());

            var controller = new CustomersController(mock.Object);
            controller.ModelState.AddModelError("FirstName", "First name is required");

            //Act
            var result = await controller.Create(new Customer());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsViewResult_WhenModelStateIsValid()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.CreateCustomer(It.IsAny<Customer>()).Result).Returns(GetAllMockCustomers().First()).Verifiable();

            var controller = new CustomersController(mock.Object);
            //Act
            var result = await controller.Create(new Customer { FirstName="Test2"});

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify();
        }


        [Fact]
        public async Task Update_ReturnsViewResult_WhenCustomerIdIsPresent()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.UpdateCustomer(It.IsAny<Customer>()).Result).Returns(GetAllMockCustomers().First()).Verifiable();

            var controller = new CustomersController(mock.Object);
            //Act
            var result = await controller.Edit(1,new Customer { CustomerId =1, FirstName = "Test2" });

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify();
        }

        [Fact]
        public async Task Update_ReturnsViewResult_WhenCustomerIdIsNotPresent()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.UpdateCustomer(It.IsAny<Customer>()).Result).Returns(GetAllMockCustomers().First()).Verifiable();

            var controller = new CustomersController(mock.Object);
            //Act
            var result = await controller.Edit(1, new Customer { FirstName = "Test2" });

            // Assert
            var notFound = Assert.IsType<NotFoundResult>(result);
            Assert.IsType<NotFoundResult>(notFound);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WhenCustomerIdIsPresent()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.GetCustomer(1).Result).Returns(GetAllMockCustomers().First()).Verifiable();

            var controller = new CustomersController(mock.Object);
            //Act
            var result = await controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ViewResult>(viewResult);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WhenCustomerIdIsNotPresent()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.GetCustomer(3).Result).Returns(GetAllMockCustomers().First()).Verifiable();

            var controller = new CustomersController(mock.Object);
            //Act
            var result = await controller.Delete(1);

            // Assert
            var notFound = Assert.IsType<NotFoundResult>(result);
            Assert.IsType<NotFoundResult>(notFound);
        }

        [Fact]
        public async Task DeleteConfirmed_ReturnsViewResult_WhenCustomerIdIsPresent()
        {
            var mock = new Mock<ICustomerHandler>();
            mock.Setup(handler => handler.UpdateCustomer(1).Result).Returns(GetAllMockCustomers().First()).Verifiable();

            var controller = new CustomersController(mock.Object);
            //Act
            var result = await controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify();
        }

        private IEnumerable<Customer> GetAllMockCustomers()
        {
            var result = new List<Customer>()
            {
                new Customer
                {
                    CustomerId =1,
                    FirstName ="Test",
                    LastName="Test",
                    Email="Test@test.com",
                    PhoneNumber="11111111111",
                    Status = Status.Active
                },
                new Customer
                {
                    CustomerId =2,
                    FirstName ="Test1",
                    LastName="Test1",
                    Email="Test1@test.com",
                    PhoneNumber="2222222222",
                    Status = Status.Active
                },
            };

            return result;
        }       
    }
}
