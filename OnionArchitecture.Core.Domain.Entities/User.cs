﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnionArchitecture.Core.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RegisterMethod { get; set; }
        public string Password { get; set; }

    }
}