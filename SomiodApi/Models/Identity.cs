using System;
using System.Xml.Serialization;

namespace SOMIOD.Models
{
    public abstract class Identity
    {
        [XmlElement("id")]
        public int? Id { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("creation_datetime")]
        public DateTime? CreationDatetime { get; set; }
    }
}