using System;

namespace TeamConnect.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Photo { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }

        public string TimeZoneId { get; set; }

        public string CountryCode { get; set; }

        public string Position { get; set; }

        public DateTime StartWorkTime { get; set; }

        public DateTime EndWorkTime { get; set; }

        public bool IsAccountCreated { get; set; }
    }
}
