﻿using AutoMapper;
using BpmContactManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BpmContactManager.Models.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        public string ServiceId { get; set; }

        public string Name { get; set; }

        public string MobilePhone { get; set; }

        public string Dear { get; set; }

        public string JobTitle { get; set; }

        public string BirthDate { get; set; }

        public ContactEntity ToEntity()
        {
            return Mapper.Map<ContactViewModel, ContactEntity>(this);
        }
    }
}