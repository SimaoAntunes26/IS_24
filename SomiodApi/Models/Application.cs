using System;

namespace SOMIOD.Models
{
    public class Application : Identity
    {
        public Application() { }

        public Application(int id, string name, DateTime creationDatetime) {
            Id = id;
            Name = name;
            CreationDatetime = creationDatetime;
        }

        public Application(string name)
        {
            Name = name;
        }
    }
}