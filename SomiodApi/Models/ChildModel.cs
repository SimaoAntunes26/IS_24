using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SOMIOD.Models
{
    public abstract class ChildModel : Identity
    {
        [XmlElement("parent")]
        public int? ParentId { get; set; }
    }
}