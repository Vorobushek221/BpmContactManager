using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BpmContactManager.Models
{
    public static class GlobalConstants
    {
        public const string ServerUri = @"http://dev.altaras.ru:8001/0/ServiceModel/EntityDataService.svc";

        public const string AuthServiceUri = @"http://dev.altaras.ru:8001/serviceModel/AuthService.svc/Login";

        public const string ServiceLogin = @"Supervisor";

        public const string ServicePassord = @"Supervisor__";
    }
}