namespace SunPOSData.Models
{
    public class User
    {
        #region Public Properties

        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        #endregion
    }
}
