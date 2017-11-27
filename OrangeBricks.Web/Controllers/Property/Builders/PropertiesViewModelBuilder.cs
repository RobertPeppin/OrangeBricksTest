using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class PropertiesViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;
        private string CurrentUserId = "";
        public PropertiesViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public PropertiesViewModel Build(PropertiesQuery query, string userId = "")
        {
            CurrentUserId = userId;
            var properties = _context.Properties
                .Where(p => p.IsListedForSale);

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                properties = properties.Where(x => x.StreetName.Contains(query.Search) 
                    || x.Description.Contains(query.Search));
            }

            
            return new PropertiesViewModel
            {
                Properties = properties
                    .ToList()
                    .Select(MapViewModel)
                    .ToList(),
                Search = query.Search
            };
        }

        private PropertyViewModel MapViewModel(Models.Property property)
        {
            var vm = new PropertyViewModel
            {
                Id = property.Id,
                StreetName = property.StreetName,
                Description = property.Description,
                NumberOfBedrooms = property.NumberOfBedrooms,
                PropertyType = property.PropertyType,
                Offers = property.Offers?.Where (o=>o.OfferingUserId == CurrentUserId).ToList()
            };

            return vm;
        }
    }
}