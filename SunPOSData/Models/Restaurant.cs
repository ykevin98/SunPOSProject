﻿namespace SunPOSData.Models
{
    public class Restaurant
    {
        #region Public Properties

        public Guid RestaurantID { get; set; }
        public string RestaurantViewName { get; set; }
        public string RestaurantLocation { get; set; }
        public string RestaurantPhoneNumber { get; set; }
        public string RestaurantName { get; set; }

        #endregion
    }
}
