using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Salesforce.Common;
using Salesforce.Common.Models.Json;
using Salesforce.Force;
using SportizeMvc.Models;

namespace SportizeMvc.DAL
{
    public class SalesforceConnect
    {
        private static string _clientId = "3MVG9vrJTfRxlfl5fRe2VYx1W96BNk9oBP4ZpXLskMKXpo6gVi9TepDaTw7XvXPw2UGU.mcAk2vakv6UoT7E5";
        private static string _clientSecret = "1964987766708022753";

        private static string _userName = "ericcarmobarbosa@fiap.com";
        private static string _accessToken = "00D5A0000015ihV!AR8AQJCA3A7skszYhzz0rJq5BOW63lFpVeATe8.9tMRgTtLLYWQJfSCcGf2AUyPDyukCloe7X6AM0dw9.M7o2jCi3cxK5EUO";
        private static string _securityToken = "qs432IRmmUFl7injQz24c4lVJ";
        private static string _password = "tantofaz8ou8";

        private static string _userAgent = "SportizeMvc";
        private static string _tokenRequestEndpointUrl = "https://login.salesforce.com/services/oauth2/token";

        public static ForceClient getClient()
        {
            var auth = new AuthenticationClient();
            auth.UsernamePasswordAsync(_clientId, _clientSecret, _userName, _password).Wait();

            return new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);
        }

        // GROUPS
        public static async Task<List<Group>> GetGroupsAsync()
        {
            string query = "SELECT id,Name,group_description__c FROM group__c";

            var client = getClient();

            var result = await client.QueryAsync<dynamic>(query);

            List<Group> groups = new List<Group>();

            foreach(var group in result.Records)
            {
                groups.Add(new Group { ID = group.Id, Name = group.Name, Description = group.group_description__c });
            }

            return groups;
        }

        public static async Task<Group> GetGroupByIdAsync(string id)
        {
            string query = "SELECT id,Name,group_description__c FROM group__c WHERE Id = '" + id + "'";

            var client = getClient();

            var result = await client.QueryAsync<dynamic>(query);

            var group = result.Records.FirstOrDefault();

            return new Group { ID = group.Id, Name = group.Name, Description = group.group_description__c };
        }

        public static async Task<SuccessResponse> AddGroupAsync(Group group)
        {
            var client = getClient();

            var group__c = new { Name = group.Name, group_description__c = group.Description };
            return await client.CreateAsync("group__c", group__c);
        }

        public static async Task<SuccessResponse> UpdateGroupAsync(Group group)
        {
            var client = getClient();

            var group__c = new { Name = group.Name, group_description__c = group.Description };

            return await client.UpdateAsync("group__c", group.ID, group__c);
        }

        public static async Task<bool> DeleteGroupAsync(String Id)
        {
            var client = getClient();
            return await client.DeleteAsync("group__c", Id);
        }

        // PLAYERS
        public static async Task<List<Player>> GetPlayersAsync()
        {
            string query = "SELECT Id, Name, email__c, state__c, city__c, neighborhood__c, address__c FROM player__c";

            var client = getClient();

            var result = await client.QueryAsync<dynamic>(query);

            List<Player> players = new List<Player>();

            foreach (var player in result.Records)
            {
                players.Add(new Player {
                    ID = player.Id,
                    Name = player.Name,
                    Email = player.email__c,
                    State = player.state__c,
                    City = player.city__c,
                    Neighborhood = player.neighborhood__c,
                    Address = player.address__c,
                });
            }

            return players;
        }

        public static async Task<Player> GetPlayerByIdAsync(string id)
        {
            string query = "SELECT Id, Name, email__c, state__c, city__c, neighborhood__c, address__c FROM player__c WHERE Id = '" + id + "'";

            var client = getClient();

            var result = await client.QueryAsync<dynamic>(query);

            var player = result.Records.FirstOrDefault();

            return new Player {
                ID = player.Id,
                Name = player.Name,
                Email = player.email__c,
                State = player.state__c,
                City = player.city__c,
                Neighborhood = player.neighborhood__c,
                Address = player.address__c,
            };
        }

        public static async Task<SuccessResponse> AddPlayerAsync(Player player)
        {
            var client = getClient();

            var player__c = new {
                Name = player.Name,
                password__c = player.Password,
                email__c = player.Email,
                state__c = player.State,
                city__c = player.City,
                neighborhood__c = player.Neighborhood,
                address__c = player.Address,
            };

            return await client.CreateAsync("player__c", player__c);
        }

        public static async Task<SuccessResponse> UpdatePlayerAsync(Player player)
        {
            var client = getClient();

            var player__c = new
            {
                Name = player.Name,
                email__c = player.Email,
                state__c = player.State,
                city__c = player.City,
                neighborhood__c = player.Neighborhood,
                address__c = player.Address,
            };

            return await client.UpdateAsync("player__c", player.ID, player__c);
        }

        public static async Task<bool> DeletePlayerAsync(String Id)
        {
            var client = getClient();
            return await client.DeleteAsync("player__c", Id);
        }

        // EVENTS
        public static async Task<List<Event>> GetEventsAsync()
        {
            string query = "SELECT id, Name, description__c, event_date__c, event_time__c, state__c, event_city__c, event_neighborhood__c, event_address__c FROM event__c";

            var client = getClient();

            var result = await client.QueryAsync<dynamic>(query);

            List<Event> events = new List<Event>();

            foreach (var evento in result.Records)
            {
                DateTime dateOnly = evento.event_date__c;
                DateTime timeOnly = evento.event_time__c;

                DateTime dateTime = dateOnly.Date.Add(timeOnly.TimeOfDay);

                events.Add(new Event
                {
                    ID = evento.Id,
                    Name = evento.Name,
                    Description = evento.description__c,
                    EventDate = dateTime,
                    State = evento.state__c,
                    City = evento.event_city__c,
                    Neighborhood = evento.event_neighborhood__c,
                    Address = evento.event_address__c,
                });
            }

            return events;
        }

        public static async Task<Event> GetEventByIdAsync(string id)
        {
            string query = "SELECT id, Name, description__c, event_date__c, event_time__c, state__c, event_city__c, event_neighborhood__c, event_address__c FROM event__c WHERE Id = '" + id + "'";
            var client = getClient();

            var result = await client.QueryAsync<dynamic>(query);
            var evento = result.Records.FirstOrDefault();

            DateTime dateOnly = evento.event_date__c;
            DateTime timeOnly = evento.event_time__c;

            DateTime dateTime = dateOnly.Date.Add(timeOnly.TimeOfDay).AddHours(3);

            return new Event
            {
                ID = evento.Id,
                Name = evento.Name,
                Description = evento.description__c,
                EventDate = dateTime,
                State = evento.state__c,
                City = evento.event_city__c,
                Neighborhood = evento.event_neighborhood__c,
                Address = evento.event_address__c,
            };
        }

        public static async Task<SuccessResponse> AddEventAsync(Event evento)
        {
            var client = getClient();

            var event__c = new
            {
                Name = evento.Name,
                description__c = evento.Description,
                event_date__c = evento.EventDate.ToString("yyyy-MM-dd"),
                event_time__c = evento.EventDate.ToString("HH:mm:ss"),
                state__c = evento.State,
                event_city__c = evento.City,
                event_neighborhood__c = evento.Neighborhood,
                event_address__c = evento.Address,
            };

            return await client.CreateAsync("event__c", event__c);
        }

        public static async Task<SuccessResponse> UpdateEventAsync(Event evento)
        {
            var client = getClient();

            var event__c = new
            {
                Name = evento.Name,
                description__c = evento.Description,
                event_date__c = evento.EventDate.ToString("yyyy-MM-dd"),
                event_time__c = evento.EventDate.ToString("HH:mm:ss"),
                state__c = evento.State,
                event_city__c = evento.City,
                event_neighborhood__c = evento.Neighborhood,
                event_address__c = evento.Address,
            };

            return await client.UpdateAsync("event__c", evento.ID, event__c);
        }

        public static async Task<bool> DeleteEventAsync(String Id)
        {
            var client = getClient();
            return await client.DeleteAsync("event__c", Id);
        }
    }
}