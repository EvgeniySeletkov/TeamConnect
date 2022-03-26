﻿using System;

namespace TeamConnect.Models.Request
{
    public class RequestModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
    }
}
