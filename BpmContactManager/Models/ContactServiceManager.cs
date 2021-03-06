﻿using BpmContactManager.EntityDataService;
using BpmContactManager.Models.Entities;
using BpmContactManager.Models.Extensions;
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
    public class ContactServiceManager
    {
        private Uri serverUri;

        public ContactServiceManager()
        {
            serverUri = new Uri(GlobalConstants.ServerUri);
        }

        public IList<ContactEntity> GetContacts(int contactCount = 40, 
                                                int skipCount = 0, 
                                                string filterOption = null)
        {
            var context = new BPMonline(serverUri);

            context.SendingRequest += new EventHandler<SendingRequestEventArgs>(OnSendingRequestCookie);

            var contactList = new List<ContactEntity>();

            try
            {
                var contactsQuery = context.ContactCollection
                    .AddQueryOption("$top", contactCount)
                    .AddQueryOption("$skip", skipCount)
                    .AddQueryOption("$orderby", "Name");
                    
                if(!string.IsNullOrEmpty(filterOption))
                {
                    contactsQuery = contactsQuery.AddQueryOption("$filter", filterOption);
                }


                contactsQuery.ToList().ForEach(contact =>
                {
                    contactList.Add(new ContactEntity
                    {
                        Id = contactList.Count,
                        ServiceId = contact.Id.ToString(),
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
                throw ex;
            }
        }

        public ContactEntity CetContactById(string ServiceId)
        {
            string filterOption = string.Format("Id eq guid'{0}'", ServiceId);
            return GetContacts(1, 0, filterOption).FirstOrDefault();
        }

        public bool AddContact(ContactEntity contact)
        {
            var content = new XElement((XNamespace)GlobalConstants.Dsmd + "properties",
                          new XElement((XNamespace)GlobalConstants.Ds + "Name", contact.Name),
                          new XElement((XNamespace)GlobalConstants.Ds + "Dear", contact.Dear),
                          new XElement((XNamespace)GlobalConstants.Ds + "BirthDate", contact?.BirthDate.Value.AddServerOffset()),
                          new XElement((XNamespace)GlobalConstants.Ds + "JobTitle", contact.JobTitle),
                          new XElement((XNamespace)GlobalConstants.Ds + "MobilePhone", contact.MobilePhone));
            var entry = new XElement((XNamespace)GlobalConstants.Atom + "entry",
                        new XElement((XNamespace)GlobalConstants.Atom + "content",
                        new XAttribute("type", "application/xml"), content));

            var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/ContactCollection", serverUri));
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool RemoveContact(string contactServiceId)
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/ContactCollection(guid'{1}')", 
                serverUri, contactServiceId));
            request.Credentials = new NetworkCredential(GlobalConstants.ServiceLogin, GlobalConstants.ServicePassord);
            request.Method = "DELETE";
            using (WebResponse response = request.GetResponse())
            {
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.NoContent)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool RemoveContact(ContactEntity contact)
        {
            return RemoveContact(contact.ServiceId);
        }

        public bool ModifyContact(ContactEntity modifiedContact)
        {
            var content = new XElement((XNamespace)GlobalConstants.Dsmd + "properties",
                    new XElement((XNamespace)GlobalConstants.Ds + "Name", modifiedContact.Name),
                    new XElement((XNamespace)GlobalConstants.Ds + "Dear", modifiedContact.Dear),
                    new XElement((XNamespace)GlobalConstants.Ds + "JobTitle", modifiedContact.JobTitle),
                    new XElement((XNamespace)GlobalConstants.Ds + "BirthDate", modifiedContact?.BirthDate.Value.AddServerOffset()),
                    new XElement((XNamespace)GlobalConstants.Ds + "MobilePhone", modifiedContact.MobilePhone)
            );
            var entry = new XElement((XNamespace)GlobalConstants.Atom + "entry",
                    new XElement((XNamespace)GlobalConstants.Atom + "content",
                            new XAttribute("type", "application/xml"),
                            content)
                    );
            var request = (HttpWebRequest)HttpWebRequest.Create(string.Format("{0}/ContactCollection(guid'{1}')",
                serverUri, modifiedContact.ServiceId));
            request.Credentials = new NetworkCredential(GlobalConstants.ServiceLogin, GlobalConstants.ServicePassord);
            request.Method = "PUT";
            request.Accept = "application/atom+xml";
            request.ContentType = "application/atom+xml;type=entry";

            using (var writer = XmlWriter.Create(request.GetRequestStream()))
            {
                entry.WriteTo(writer);
            }

            using (WebResponse response = request.GetResponse())
            {
                if (((HttpWebResponse)response).StatusCode == HttpStatusCode.NoContent)
                {
                    return true;
                }
                else
                {
                    return false;
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