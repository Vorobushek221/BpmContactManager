using BpmContactManager.EntityDataService;
using BpmContactManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Net;
using System.Web;

namespace BpmContactManager.Models
{
    public class ServiceManager
    {
        private Uri serverUri;

        public ServiceManager()
        {
            serverUri = new Uri(GlobalConstants.ServerUri);
        }

        public IList<ContactEntity> GetContacts(int contactCount = 40)
        {
            var context = new BPMonline(serverUri);

            context.SendingRequest += new EventHandler<SendingRequestEventArgs>(OnSendingRequestCookie);

            var contactList = new List<ContactEntity>();

            try
            {
                var contacts = context.ContactCollection.AddQueryOption("$top", contactCount).ToList();
                contacts.ForEach(contact =>
                {
                    contactList.Add(new ContactEntity
                    {
                        Id = contactList.Count,
                        BirthDate = contact.BirthDate,
                        Dear = contact.Dear,
                        JobTitle = contact.JobTitle,
                        MobilePhone = contact.MobilePhone
                    });
                });

                return contactList;
            }
            catch (Exception ex)
            {
                // TODO handle
                return null;
            }
        }

        private void OnSendingRequestCookie(object sender, SendingRequestEventArgs e)
        {
            LoginManager.TryLogin(GlobalConstants.ServiceLogin, GlobalConstants.ServicePassord);
            var req = e.Request as HttpWebRequest;
            req.CookieContainer = LoginManager.AuthCookie;
            e.Request = req;
        }
    }
}