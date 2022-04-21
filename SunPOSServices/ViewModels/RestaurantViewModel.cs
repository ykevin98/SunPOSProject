namespace SunPOSServices.ViewModels
{
    public class RestaurantViewModel
    {
        #region Public Properties

        public Guid RestaurantID { get; set; }
        public string RestaurantViewName { get; set; }
        public string RestaurantLocation { get; set; }
        public string RestaurantPhoneNumber { get; set; }
        public string RestaurantName { get; set; }
        public string MondayHours { get; set; }
        public string TuesdayHours { get; set; }
        public string WednesdayHours { get; set; }
        public string ThursdayHours { get; set; }
        public string FridayHours { get; set; }
        public string SaturdayHours { get; set; }
        public string SundayHours { get; set; }

        #endregion
    }
}
