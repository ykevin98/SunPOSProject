namespace SunPOSServices.ViewModels
{
    public class CartViewModel
    { 
        #region Public Properties

        public int MenuId { get; set; }
        public string Item { get; set; }
        public string Item2 { get; set; }
        public decimal Price { get; set; }
        public decimal LunchPrice { get; set; }
        public decimal DinnerPrice { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public Guid RestaurantID { get; set; }
        public Guid ItemId { get; set; }

        #endregion
    }
}
