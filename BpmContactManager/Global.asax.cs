using AutoMapper;
using BpmContactManager.Models;
using BpmContactManager.Models.Entities;
using BpmContactManager.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BpmContactManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ContactEntity, ContactViewModel>()
                    .ForMember(dest => dest.BirthDate,
                        opt => opt.MapFrom(src => (src.BirthDate != null)
                            ? ((src.BirthDate.Value != default(DateTime)) ? src.BirthDate.Value.ToString(GlobalConstants.DateFormat) : string.Empty)
                            : string.Empty));
                cfg.CreateMap<ContactViewModel, ContactEntity>()
                        .ForMember(dest => dest.BirthDate,
                            opt => opt.MapFrom(src => (!string.IsNullOrEmpty(src.BirthDate))
                                ? DateTime.Parse(src.BirthDate)
                                : default(DateTime?)));

            });

        }

        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            //log the error!
        }
    }
}
