using SOMIOD.Common;
using SOMIOD.Models;
using SomiodApi.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace SOMIOD.Controllers
{
    public partial class SomiodController : ApiRoutes
    {
        private const string notificationTable = "Notifications";
        private int NotificationLastId => SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"SELECT IDENT_CURRENT('{notificationTable}')", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read() || reader[0] == DBNull.Value)
                        return 0;

                    return Convert.ToInt32(reader[0]);
                });

        private static List<string> leftoverNotifs = new List<string>();
        private static Dictionary<int, List<string>> createNotifs = GetNotificationEndpointsForEventType(EventType.creation);
        private static Dictionary<int, List<string>> deleteNotifs = GetNotificationEndpointsForEventType(EventType.deletion);


        [HttpGet]
        [Route("{applicationName}/{containerName}/notif/{name}", Name = "GetNotif")]
        public IHttpActionResult GetNotification(string applicationName, string containerName, string name)
        {
            try
            {
                var result = FindNotification(name, containerName, applicationName);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("{applicationName}/{containerName}/notif")]
        public IHttpActionResult CreateNotification(string applicationName, string containerName, [FromBody] Notification notification)
        {
            if (notification == null || notification?.Event == null || notification?.Endpoint == null)
                return BadRequest("Body needs to contain at least notification event and endpoint");

            EventType eventType;
            if (!Enum.TryParse(notification.Event, out eventType))
                return BadRequest($"Invalid event type. Possible values: {string.Join(", ", Enum.GetNames(typeof(EventType)))}");

            try
            {
                Container container = FindContainer(containerName, applicationName);
                if (container == null)
                    return NotFound();

                string name = notification?.Name;
                if (notification == null || !UrlNameValidation(name))
                    name = (NotificationLastId + 1).ToString();
                if (ExistsNotification(name))
                    name = $"{name}_{NotificationLastId + 1}";

                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO {notificationTable}(Name, Creation_Datetime, Container_Id, Event, Endpoint, Enabled) VALUES(@name, @creation_datetime, @parent, @event, @endpoint, @enabled)", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@creation_datetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@parent", container.Id);
                    cmd.Parameters.AddWithValue("@event", (byte)eventType);
                    cmd.Parameters.AddWithValue("@endpoint", notification.Endpoint);
                    cmd.Parameters.AddWithValue("@enabled", true);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                var notif = FindNotification(name, container.Name, applicationName);
                AddNotifToDictionary(eventType, notif.Endpoint, (int)notif.ParentId);
                return CreatedAtRoute("GetNotif", new { name = notif.Name }, notif);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{applicationName}/{containerName}/notif/{name}")]
        public IHttpActionResult DeleteNotification(string applicationName, string containerName, string name)
        {
            try
            {
                Notification notification = FindNotification(name, containerName, applicationName);
                if (notification == null)
                    return NotFound();

                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM {notificationTable} WHERE Name = @name", conn);
                    cmd.Parameters.AddWithValue("@name", name);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                RemoveNotifFromDictionary(notification.Endpoint, (int)notification.ParentId);
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch
            {
                return InternalServerError();
            }
        }

        public static void DeleteAllNotifications(SqlCommand cmd, List<string> names)
        {
            if (names.Count == 0)
                return;

            string nameList = string.Join(",", names);

            cmd.CommandText = $"SELECT Endpoint FROM {notificationTable} WHERE Name IN (@names)";
            cmd.Parameters.AddWithValue("@names", nameList);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                leftoverNotifs.Add(reader.GetString(0));
            }
            reader.Close();
            cmd.Parameters.Clear();

            cmd.CommandText = $"DELETE FROM {notificationTable} WHERE Name IN (@names)";
            cmd.Parameters.AddWithValue("@names", nameList);

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        public static Notification FindNotification(string name, string parentName, string grandparentName)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT t.* FROM {notificationTable} t" +
                    $" JOIN Containers c ON c.id = t.Container_Id " +
                    $" JOIN Applications a ON a.id = c.Application_Id" +
                    $" WHERE t.Name = @name AND c.Name = @parent_name AND a.Name = @grandparent_name", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@parent_name", parentName);
                cmd.Parameters.AddWithValue("@grandparent_name", grandparentName);

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                    return null;

                Notification notification = new Notification
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    CreationDatetime = (DateTime)reader["Creation_Datetime"],
                    ParentId = (int)reader["Container_Id"],
                    Event = Enum.GetName(typeof(EventType), (byte)reader["Event"]),
                    Endpoint = (string)reader["Endpoint"],
                    Enabled = (bool)reader["Enabled"]
                };

                return notification;
            });
        }

        public static bool ExistsNotification(string name)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {notificationTable}" +
                    $" WHERE Name = @name", conn);
                cmd.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                return (int)reader[0] > 0;
            });
        }

        private static Dictionary<int, List<string>> GetNotificationEndpointsForEventType(EventType eventType)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                Dictionary<int, List<string>> notifs = new Dictionary<int, List<string>>();

                SqlCommand cmd = new SqlCommand($"SELECT Container_Id, Endpoint FROM {notificationTable} WHERE Event IN ({(int)eventType}, {(int)EventType.both})", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                int containerId = 0;
                string endpoint = "";
                while (reader.Read())
                {
                    containerId = (int)reader[0];
                    endpoint = (string)reader[1];
                    if (notifs.ContainsKey(containerId))
                        notifs[containerId].Add(endpoint);
                    else
                        notifs.Add(containerId, new List<string>() { endpoint });
                }

                return notifs;
            });
        }

        private static void AddNotifToDictionary(EventType eventType, string endpoint, int containerId)
        {
            if (eventType == EventType.creation || eventType == EventType.both)
            {
                if (createNotifs.ContainsKey(containerId))
                    createNotifs[containerId].Add(endpoint);
                else
                    createNotifs.Add(containerId, new List<string>() { endpoint });
            }
            if (eventType == EventType.creation || eventType == EventType.both)
            {
                if (deleteNotifs.ContainsKey(containerId))
                    deleteNotifs[containerId].Add(endpoint);
                else
                    deleteNotifs.Add(containerId, new List<string>() { endpoint });
            }
        }

        private static void RemoveNotifFromDictionary(string endpoint, int containerId)
        {
            if (createNotifs.ContainsKey(containerId))
                createNotifs[containerId].Remove(endpoint);
            if (deleteNotifs.ContainsKey(containerId))
                deleteNotifs[containerId].Remove(endpoint);
        }

        private static void RemoveNotifsFromDictionary(List<string> endpoints)
        {
            foreach (List<string> list in createNotifs.Values)
            {
                list.RemoveAll(e => endpoints.Contains(e));
            }
            foreach (List<string> list in deleteNotifs.Values)
            {
                list.RemoveAll(e => endpoints.Contains(e));
            }
        }

        public static void ClearLeftoverNotifs(bool fullClear = true)
        {
            if (fullClear)
                RemoveNotifsFromDictionary(leftoverNotifs);
            leftoverNotifs.Clear();
        }
    }
}