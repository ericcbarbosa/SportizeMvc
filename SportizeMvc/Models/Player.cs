using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SportizeMvc.Models
{
    public class Player
    {
        public string ID { get; set; }

        [DisplayName("Nome")]
        public string Name { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Senha")]
        public string Password { get; set; }

        [DisplayName("Estado")]
        public string State { get; set; }

        [DisplayName("Cidade")]
        public string City { get; set; }

        [DisplayName("Bairro")]
        public string Neighborhood { get; set; }

        [DisplayName("Endereço")]
        public string Address { get; set; }

        [DisplayName("Participa dos grupos")]
        public virtual ICollection<Group> Groups { get; set; }

        public Player(string iD, string name, string email, string password, string state, string city, string neighborhood, string address, ICollection<Group> groups)
        {
            ID = iD;
            Name = name;
            Email = email;
            Password = password;
            State = state;
            City = city;
            Neighborhood = neighborhood;
            Address = address;
            Groups = groups;
        }

        public Player(string iD, string name, string email, string password, string state, string city, string neighborhood, string address)
        {
            ID = iD;
            Name = name;
            Email = email;
            Password = password;
            State = state;
            City = city;
            Neighborhood = neighborhood;
            Address = address;
        }

        public Player()
        {
        }
    }
}