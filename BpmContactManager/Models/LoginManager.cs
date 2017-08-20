using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace BpmContactManager.Models
{
    public static class LoginManager
    {
        public static string AuthServiceUri { get; private set; }

        public static CookieContainer AuthCookie { get; set; }

        static LoginManager()
        {
            AuthServiceUri = GlobalConstants.AuthServiceUri;

            AuthCookie = new CookieContainer();
        }


        public static bool TryLogin(string userName, string userPassword)
        {
            var authRequest = HttpWebRequest.Create(AuthServiceUri) as HttpWebRequest;
            authRequest.Method = "POST";
            authRequest.ContentType = "application/json";
            authRequest.CookieContainer = AuthCookie;

            using (var requestStream = authRequest.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    writer.Write(@"{
                                        ""UserName"":""" + userName + @""",
                                        ""UserPassword"":""" + userPassword + @"""
                                    }");
                }
            }

            using (var response = (HttpWebResponse)authRequest.GetResponse())
            {
                if (AuthCookie.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}