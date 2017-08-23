using AutoMapper;
using BpmContactManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BpmContactManager.Models.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }

        public string ServiceId { get; set; }

        [Display(Name="Name")]
        public string Name { get; set; }

        [Display(Name = "Mobile phone")]
        public string MobilePhone { get; set; }

        [Display(Name = "Dear")]
        public string Dear { get; set; }

        [Display(Name = "Job title")]
        public string JobTitle { get; set; }

        [Display(Name = "Birth date")]
        public string BirthDate { get; set; }

        public ContactEntity ToEntity()
        {
            return Mapper.Map<ContactViewModel, ContactEntity>(this);
        }
    }
}