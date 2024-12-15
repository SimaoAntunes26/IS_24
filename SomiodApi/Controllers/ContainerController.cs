using SOMIOD.Common;
using SOMIOD.Models;
using SomiodApi.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace SOMIOD.Controllers
{
    public partial class SomiodController : ApiRoutes
    {
        private const string containersTable = "Containers";
        private int ContainersLastId => SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"SELECT IDENT_CURRENT('{containersTable}')", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read() || reader[0] == DBNull.Value)
                        return 0;

                    return Convert.ToInt32(reader[0]);
                });

        [HttpGet]
        [Route("{applicationName}/{name}", Name = "GetContainer")]
        public IHttpActionResult GetContainer(string applicationName, string name)
        {
            if (Request.Headers.Contains(specialHeader))
                switch (Request.Headers.GetValues(specialHeader).First())
                {
                    case "record":
                        return Ok(SqlHelper.GetChildsOfType(LocateType.CONT_RECORDS, name, applicationName));
                    case "notification":
                        return Ok(SqlHelper.GetChildsOfType(LocateType.CONT_NOTIFICATIONS, name, applicationName));
                    default:
                        return BadRequest("Locator unknown");
                }

            try
            {
                var result = FindContainer(name, applicationName);
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
        [Route("{applicationName}")]
        public IHttpActionResult CreateContainer(string applicationName, [FromBody] Container container)
        {
            try
            {
                Application application = FindApplication(applicationName);
                if (application == null)
                    return NotFound();

                int test = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"SELECT MAX(Id) FROM {containersTable}", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read() || reader[0] == DBNull.Value)
                        return 0;

                    return (int)reader[0];
                });

                string name = container?.Name;
                if (container == null || !UrlNameValidation(name))
                    name = (ContainersLastId + 1).ToString();
                if (ExistsContainer(name))
                    name = $"{name}_{ContainersLastId + 1}";

                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO {containersTable}(Name, Creation_Datetime, Application_Id) VALUES(@name, @creation_datetime, @parent)", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@creation_datetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@parent", application.Id);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                var cont = FindContainer(name, application.Name);
                return CreatedAtRoute("GetContainer", new { name = cont.Name }, cont);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("{applicationName}/{name}")]
        public IHttpActionResult UpdateContainer(string applicationName, string name, [FromBody] Container container)
        {
            try
            {
                Container oldContainer = FindContainer(name, applicationName);
                if (oldContainer == null)
                    return NotFound();

                if (!UrlNameValidation(container?.Name))
                    return BadRequest();

                string newName = container?.Name;
                if (newName == null)
                    newName = $"{oldContainer.Id}";
                else if (oldContainer.Name == newName || ExistsApplication(newName))
                    BadRequest("Name is the same or already exists");


                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE {containersTable} SET Name = @name WHERE Name = @old_name", conn);
                    cmd.Parameters.AddWithValue("@name", newName);
                    cmd.Parameters.AddWithValue("@old_name", oldContainer.Name);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                return Ok(FindContainer(newName, applicationName));
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{applicationName}/{name}")]
        public IHttpActionResult DeleteContainer(string applicationName, string name)
        {
            try
            {
                if (NotExistsContainer(name, applicationName))
                    return NotFound();

                SqlHelper.TransactionStarter(cmd =>
                {
                    DeleteAllRecords(cmd, SqlHelper.GetChildsOfType(cmd, LocateType.CONT_RECORDS, name, applicationName));
                    DeleteAllNotifications(cmd, SqlHelper.GetChildsOfType(cmd, LocateType.CONT_NOTIFICATIONS, name, applicationName));

                    cmd.CommandText = $"DELETE FROM {containersTable} WHERE Name = @name";
                    cmd.Parameters.AddWithValue("@name", name);

                    cmd.ExecuteNonQuery();
                });

                ClearLeftoverNotifs();
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch
            {
                ClearLeftoverNotifs(false);
                return InternalServerError();
            }
        }

        public static void DeleteAllContainers(SqlCommand cmd, List<string> names, string parentName)
        {
            if (names.Count == 0)
                return;

            foreach (var name in names)
            {
                DeleteAllRecords(cmd, SqlHelper.GetChildsOfType(cmd, LocateType.CONT_RECORDS, name, parentName));
                DeleteAllNotifications(cmd, SqlHelper.GetChildsOfType(cmd, LocateType.CONT_NOTIFICATIONS, name, parentName));
            }

            string nameList = string.Join(",", names);

            cmd.CommandText = $"DELETE FROM {containersTable} WHERE Name IN (@names)";
            cmd.Parameters.AddWithValue("@names", nameList);

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        public static Container FindContainer(string name, string parentName)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT t.* FROM {containersTable} t" +
                    $" JOIN Applications a ON a.id = t.Application_Id" +
                    $" WHERE t.Name = @name AND a.Name = @parent_name", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@parent_name", parentName);

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                    return null;

                Container container = new Container
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    CreationDatetime = (DateTime)reader["Creation_Datetime"],
                    ParentId = (int)reader["Application_Id"]
                };

                return container;
            });
        }

        public static bool NotExistsContainer(string name, string parentName)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {containersTable} t" +
                    $" JOIN Applications a ON a.id = t.Application_Id" +
                    $" WHERE t.Name = @name AND a.Name = @parent_name", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@parent_name", parentName);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                return (int)reader[0] == 0;
            });
        }
        public static bool ExistsContainer(string name)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {containersTable}" +
                    $" WHERE Name = @name", conn);
                cmd.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                return (int)reader[0] > 0;
            });
        }
    }
}