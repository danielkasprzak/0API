namespace _0API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string HWID { get; set; }
        public int IsBanned { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
