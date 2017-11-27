using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class BookViewingViewModel
    {
        public string PropertyType { get; set; }
        public string StreetName { get; set; }
        public DateTime Appointment { get; set; }
        public int PropertyId { get; set; }
    }
}