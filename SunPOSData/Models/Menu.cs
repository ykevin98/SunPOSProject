namespace SunPOSData.Models
{
    public class Menu
    {
        #region Public Properties

        public int MenuId { get; set; }
        public string Category { get; set; }
        public string Category2 { get; set; }
        public string Item { get; set; }
        public string Item2 { get; set; }
        public decimal Price { get; set; }
        public decimal LunchPrice { get; set; }
        public decimal DinnerPrice { get; set; }
        public string Description { get; set; }
        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Guid RestaurantID { get; set; }

        #endregion
    }
}
