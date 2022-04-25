using System;

namespace TeamConnect.Models.Leave
{
    public class LeaveModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
    }
}
