namespace SunPOSData.Common
{
    public static class Constants
    {
        #region TableName Static Class

        public static class TableName
        {
            public const string Categories = "Categories";
            public const string Restaurants = "Restaurants";
            public const string User = "User";
            public const string ShoppingCart = "ShoppingCart";
        }

        #endregion

        #region StoredProcedure Static Class

        public static class StoredProcedures
        {
            public const string GetMenu = "dbo.GetMenu @RestaurantName";
        }

        #endregion
    }
}
