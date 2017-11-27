using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookViewingCommand
    {
        public int PropertyId { get; set; }
        public string BuyerId { get; set; }
        public DateTime Appointment { get; set; }
    }
}