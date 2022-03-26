using System;

namespace TeamConnect.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Photo { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime StartWorkTime { get; set; }

        public DateTime EndWorkTime { get; set; }
    }
}
