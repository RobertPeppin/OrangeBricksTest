using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public BookViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(BookViewingCommand command)
        {

            var property = _context.Properties.Find(command.PropertyId);

            // TODO: last chance to look for double bookings.
            var viewing = new Viewing
            {
                AppointmentTime = command.Appointment,
                BuyerId = command.BuyerId
            };

            if (property.Viewings == null)
            {
                property.Viewings = new List<Viewing>();
            }

            property.Viewings.Add(viewing);

            // as part of this a notification should be sent to the seller, and comfirmation sent to the buyer
            _context.SaveChanges();
        }
    }
}