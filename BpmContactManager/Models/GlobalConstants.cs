using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BpmContactManager.Models
{
    public static class GlobalConstants
    {
        public const string ServerUri = @"http://dev.altaras.ru:8001/0/ServiceModel/EntityDataService.svc";

        public const string AuthServiceUri = @"http://dev.altaras.ru:8001/serviceModel/AuthService.svc/Login";

        public const string ServiceLogin = @"Supervisor";

        public const string ServicePassord = @"Supervisor__";

        public const string ds = @"http://schemas.microsoft.com/ado/2007/08/dataservices";

        public const string dsmd = @"http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";

        public const string atom = @"http://www.w3.org/2005/Atom";
    }
}