namespace SunPOSData.Models
{
    public class User
    {
        #region Public Properties

        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }


        #endregion
    }
}
