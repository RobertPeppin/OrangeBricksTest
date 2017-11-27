using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class BookViewingViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public BookViewingViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public BookViewingViewModel Build(int id)
        {
            var property = _context.Properties.Find(id);

            return new BookViewingViewModel
            {
                PropertyId = property.Id,
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                Appointment = DateTime.Now.AddDays(2) // TODO find a suitable time in the sellers diary
            };
        }
    }
}