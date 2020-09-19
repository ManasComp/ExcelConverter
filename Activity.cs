﻿using System;

namespace TimeManagement.Models
{
    public class Activity
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public string Name { get; set; }
    }
}