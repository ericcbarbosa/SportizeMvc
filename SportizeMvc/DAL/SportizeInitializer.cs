using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportizeMvc.Models;

namespace SportizeMvc.DAL
{
    public class SportizeInitializer : System.Data.Entity.DropCreateDatabaseAlways<SportizeContext>
    {
        protected override void Seed(SportizeContext context)
        {

            // Mock Groups
            var groups = new List<Group>
            {
                new Group { ID="1", Name="Grupo A", Description="Equipe fantastica do grupo A, até o momento um dos mais ativos do Sportize" },
                new Group { ID="2", Name="Grupo B", Description="Equipe B, jogos todas as terças e quintas" },
                new Group { ID="3", Name="Grupo C", Description="Equipe fantastica do grupo A, até o momento um dos mais ativos do Sportize" },
            };

            // Mock Players
            var players = new List<Player>
            {
                new Player { ID="1", Name="Eric", Email="ericcarmobarbosa@gmail.com", Password="Abc123$", State="São Paulo", City="São Paulo", Neighborhood="Vila Teste", Address="Rua Teste, 123" },
                new Player { ID="2", Name="Bruno", Email="bruno@gmail.com", Password="Abc123$", State="São Paulo", City="São Paulo", Neighborhood="Vila Outro Teste", Address="Rua Outro teste, 123" },
                new Player { ID="3", Name="Mariana", Email="mariana@gmail.com", Password="Abc123$", State="São Paulo", City="São Paulo", Neighborhood="Vila Mariana", Address="Rua Mariana, 123" },
            };

            // Mock Events
            var events = new List<Event>
            {
                new Event { ID="1", Name="Grupo A vs Grupo B", Description="Uma bela duma partida", State="São Paulo", City="São Paulo", Neighborhood="Vila Teste", Address="Rua Teste, 123", EventDate=DateTime.Parse("2018-11-10T10:00:00-5:00") },
                new Event { ID="2", Name="Grupo B vs Grupo C", Description="Um classico do Sportize", State="São Paulo", City="São Paulo", Neighborhood="Vila Teste", Address="Rua Teste, 123", EventDate=DateTime.Parse("2018-11-10T12:00:00-5:00") },
                new Event { ID="3", Name="Grupo A vs Grupo C", Description="A grande final", State="São Paulo", City="São Paulo", Neighborhood="Vila Teste", Address="Rua Teste, 123", EventDate=DateTime.Parse("2018-11-10T07:00:00-5:00") },
            };

            // Add everything to context
            players.ForEach(s =>
            {
                s.Groups = groups;
                context.Players.Add(s);
            });

            groups.ForEach(s =>
            {
                s.Events = events;
                s.Players = players;
                context.Groups.Add(s);
            });

            events.ForEach(s =>
            {
                s.Groups = groups;
                context.Events.Add(s);
            });

            // Save things
            context.SaveChanges();
        }

    }
}