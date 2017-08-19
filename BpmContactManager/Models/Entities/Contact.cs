﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BpmContactManager.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        public string MobilePhone { get; set; }

        public string Dear { get; set; }

        public string JobTitle { get; set; }

        public DateTime BirthDate { get; set; }
    }
}