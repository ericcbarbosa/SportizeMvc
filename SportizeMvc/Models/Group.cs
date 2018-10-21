using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SportizeMvc.Models
{
    public class Group
    {
        public string ID { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Integrantes")]
        public virtual ICollection<Player> Players { get; set; }

        [DisplayName("Eventos")]
        public virtual ICollection<Event> Events { get; set; }

        public Group(string iD, string name, string description, ICollection<Player> players, ICollection<Event> events)
        {
            ID = iD;
            Name = name;
            Description = description;
            Players = players;
            Events = events;
        }

        public Group(string iD, string name, string description)
        {
            ID = iD;
            Name = name;
            Description = description;
        }

        public Group()
        {
        }
    }
}