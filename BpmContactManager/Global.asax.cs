using AutoMapper;
using BpmContactManager.Models;
using BpmContactManager.Models.Entities;
using BpmContactManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BpmContactManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ContactEntity, ContactViewModel>()
                    .ForMember(dest => dest.BirthDate,
                        opt => opt.MapFrom(src => (src.BirthDate != null) 
                            ? src.BirthDate.Value.ToString(GlobalConstants.DateFormat) 
                            : string.Empty));
            cfg.CreateMap<ContactViewModel, ContactEntity>()
                    .ForMember(dest => dest.BirthDate,
                        opt => opt.MapFrom(src => (!string.IsNullOrEmpty(src.BirthDate)) 
                            ? DateTime.Parse(src.BirthDate) 
                            : default(DateTime?)));

            });
        }
}
}
