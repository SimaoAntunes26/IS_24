using SOMIOD.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SOMIOD.Models
{
    public class Notification : ChildModel
    {
        [XmlElement("event")]
        public string Event { get; set; }
        [XmlElement("endpoint")]
        public string Endpoint { get; set; }
        [XmlElement("enabled")]
        public bool Enabled { get; set; }

        public Notification() { }
        public Notification(int id, string name, DateTime creationDatetime, int parentId, string eventName, string endpoint, bool enabled) {
            Id = id;
            Name = name;
            ParentId = parentId;
            CreationDatetime = creationDatetime;
            Event = eventName;
            Endpoint = endpoint;
            Enabled = enabled;
        }
    }
}