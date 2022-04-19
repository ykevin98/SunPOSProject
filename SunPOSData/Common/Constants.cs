namespace SunPOSData.Common
{
    public static class Constants
    {
        public static class TableName
        {
            public const string Categories = "Categories";
            public const string Restaurants = "Restaurants";
        }

        public static class StoredProcedures
        {
            public const string GetMenu = "dbo.GetMenu @RestaurantName";
        }
        
    }
}
