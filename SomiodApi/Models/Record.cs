using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SOMIOD.Models
{
    public class Record : ChildModel
    {
        [XmlElement("content")]
        public string Content { get; set; }

        public Record() { }
        public Record(int id, string name, DateTime creationDatetime, int parentId, string content) {
            Id = id;
            Name = name;
            CreationDatetime = creationDatetime;
            ParentId = parentId;
            Content = content;
        }
    }
}