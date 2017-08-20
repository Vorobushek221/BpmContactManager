using AutoMapper;
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
                cfg.CreateMap<ContactEntity, ContactViewModel>();
                cfg.CreateMap<ContactViewModel, ContactEntity>();
            });
        }
}
}
