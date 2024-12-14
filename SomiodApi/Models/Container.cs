using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOMIOD.Models
{
    public class Container : ChildModel
    {
        public Container() { }

        public Container(int id, string name, DateTime creationDatetime, int parentId) {
            Id = id;
            Name = name;
            CreationDatetime = creationDatetime;
            ParentId = parentId;
        }
    }
}