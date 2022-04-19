namespace SunPOSData.Models
{
    public class Category
    {
        #region Public Properties

        public Guid CategoryID { get; set; }
        public string CategoryName { get; set; }
        public Guid RestaurantID { get; set; }

        #endregion
    }
}
