namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeOfferCommand
    {
        public int PropertyId { get; set; }
        public string BuyerId { get; set;}
        public int Offer { get; set; }
    }
}