﻿using System;

namespace OnionArchitecture.Core.Domain.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Completed { get; set; }
    }
}
