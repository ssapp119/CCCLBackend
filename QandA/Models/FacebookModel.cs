namespace QandA.Models
{
    public class FacebookModel
    {
        public string accessToken { get; set; }

        public string email { get; set; }

        public string name { get; set; }
    }

    public class FacebookDatabaseModel
    {
        public int AccountID { get; set; }
        public string accessToken { get; set; }

        public string email { get; set; }

        public string name { get; set; }

    }
}
