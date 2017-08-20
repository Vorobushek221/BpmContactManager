using AutoMapper;
using BpmContactManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BpmContactManager.Models.Entities
{
    public class ContactEntity
    {
        public int Id { get; set; }

        public string ServiceId { get; set; }

        public string Name { get; set; }

        public string MobilePhone { get; set; }

        public string Dear { get; set; }

        public string JobTitle { get; set; }

        public DateTime? BirthDate { get; set; }

        public ContactViewModel ToViewModel()
        {
            return Mapper.Map<ContactEntity, ContactViewModel>(this);
        }
    }
}