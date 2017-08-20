using BpmContactManager.EntityDataService;
using BpmContactManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;

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
                        Name = contact.Name,
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

        public void AddContact(ContactEntity contact)
        {
            var content = new XElement((XNamespace)GlobalConstants.dsmd + "properties",
                          new XElement((XNamespace)GlobalConstants.ds + "Name", contact.Name),
                          new XElement((XNamespace)GlobalConstants.ds + "Dear", contact.Dear),
                          new XElement((XNamespace)GlobalConstants.ds + "BirthDate", contact.BirthDate),
                          new XElement((XNamespace)GlobalConstants.ds + "JobTitle", contact.JobTitle),
                          new XElement((XNamespace)GlobalConstants.ds + "MobilePhone", contact.MobilePhone));
            var entry = new XElement((XNamespace)GlobalConstants.atom + "entry",
                        new XElement((XNamespace)GlobalConstants.atom + "content",
                        new XAttribute("type", "application/xml"), content));

            var request = (HttpWebRequest)HttpWebRequest.Create(serverUri + "/ContactCollection");
            request.Credentials = new NetworkCredential(GlobalConstants.ServiceLogin, GlobalConstants.ServicePassord);
            request.Method = "POST";
            request.Accept = "application/atom+xml";
            request.ContentType = "application/atom+xml;type=entry";

            using (var writer = XmlWriter.Create(request.GetRequestStream()))
            {
                entry.WriteTo(writer);
            }

            using (WebResponse response = request.GetResponse())
            {
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.Created)
                {
                    // TODO handle
                }
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