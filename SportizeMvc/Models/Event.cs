using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SportizeMvc.Models
{
    public class Event
    {
        public string ID { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Data do Evento")]
        public DateTime EventDate { get; set; }

        [DisplayName("Endereço")]
        public string Address { get; set; }

        [DisplayName("Bairro")]
        public string Neighborhood { get; set; }

        [DisplayName("Cidade")]
        public string City { get; set; }

        [DisplayName("Estado")]
        public string State { get; set; }

        [DisplayName("Grupos")]
        public virtual ICollection<Group> Groups { get; set; }

        public Event(string iD, string name, string description, DateTime eventDate, string address, string neighborhood, string city, string state, ICollection<Group> groups)
        {
            ID = iD;
            Name = name;
            Description = description;
            EventDate = eventDate;
            Address = address;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Groups = groups;
        }

        public Event(string iD, string name, string description, DateTime eventDate, string address, string neighborhood, string city, string state)
        {
            ID = iD;
            Name = name;
            Description = description;
            EventDate = eventDate;
            Address = address;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }

        public Event()
        {
        }
    }
}