using NSubstitute;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class BookViewingCommandHandlerTest
    {
        private BookViewingCommandHandler _handler;
        private IOrangeBricksContext _context;
        private IDbSet<Models.Property> _properties;
        private IDbSet<Models.Viewing> _viewings;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();

            _viewings = Substitute.For<IDbSet<Models.Viewing>>();
            _context.Viewings.Returns(_viewings);
            _properties = Substitute.For<IDbSet<Models.Property>>();
            _context.Properties.Returns(_properties);
            _handler = new BookViewingCommandHandler(_context);
        }

        [Test]
        public void HandleShouldAddViewing()
        {
            // Arrange
            var builder = new PropertiesViewModelBuilder(_context);

            var property = new Models.Property { Id = 1, StreetName = "Some street", Description = "Great location", IsListedForSale = true };

            _properties.Find(1).Returns(property);

            var command = new BookViewingCommand();

            command.Appointment = DateTime.Now;
            command.PropertyId = 1;
            command.BuyerId = Guid.NewGuid().ToString();

            // Act
            _handler.Handle(command);

            // Assert
            Assert.IsTrue(property.Viewings.Count == 1);
        }
    }
}
