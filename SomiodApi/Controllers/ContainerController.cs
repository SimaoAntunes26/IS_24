using SOMIOD.Common;
using SOMIOD.Models;
using SomiodApi.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web.Http;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod")]
    public class ContainerController : ApiRoutes
    {
        private const string table = "Containers";
        private int LastId => SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"SELECT MAX(Id) FROM {table}", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                        return 0;

                    return (int)reader[0];
                });

        [HttpGet]
        [Route("{applicationName}/{name}", Name = "GetContainer")]
        public IHttpActionResult Get(string applicationName, string name)
        {
            if (Request.Headers.Contains(specialHeader))
                switch (Request.Headers.GetValues(specialHeader).First())
                {
                    case "record":
                        return Ok(SqlHelper.GetChildsOfType(LocateType.CONT_RECORDS, name));
                    case "notification":
                        return Ok(SqlHelper.GetChildsOfType(LocateType.CONT_NOTIFICATIONS, name));
                    default:
                        return BadRequest("Locator unknown");
                }

            try
            {
                var result = Find(name, applicationName);
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
        public IHttpActionResult Create(string applicationName, [FromBody] Container container)
        {
            try
            {
                Application application = ApplicationController.Find(applicationName);
                if (application == null)
                    return NotFound();

                if (!UrlNameValidation(container?.Name))
                    container.Name = (LastId + 1).ToString();
                if (container == null || Exists(container.Name))
                    container.Name = $"{container.Name}_{LastId + 1}";

                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO {table}(Name, Creation_Datetime, Application_Id) VALUES(@name, @creation_datetime, @parent)", conn);
                    cmd.Parameters.AddWithValue("@name", container.Name);
                    cmd.Parameters.AddWithValue("@creation_datetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@parent", application.Id);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                var cont = Find(container.Name, application.Name);
                return CreatedAtRoute("GetContainer", new { name = cont.Name}, cont);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("{applicationName}/{name}")]
        public IHttpActionResult Update(string applicationName, string name, [FromBody] Container container)
        {
            try
            {
                Container oldContainer = Find(name, applicationName);
                if (oldContainer == null)
                    return NotFound();

                if (container == null || !UrlNameValidation(container.Name))
                    return BadRequest();

                if (container.Name == null)
                    container.Name = $"{oldContainer.Id}";
                else if (Exists(container.Name))
                    BadRequest();

                
                bool result = SqlHelper.ConnectionStarter(conn => {    
                SqlCommand cmd = new SqlCommand($"UPDATE {table} SET Name = @name WHERE Name = @old_name", conn);
                    cmd.Parameters.AddWithValue("@name", container.Name);
                    cmd.Parameters.AddWithValue("@old_name", name);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                return Ok(Find(container.Name, applicationName));
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{applicationName}/{name}")]
        /**
         * Might rework this for a recursive delete
         */
        public IHttpActionResult Delete(string applicationName, string name)
        {
            try
            {
                if (NotExists(name, applicationName))
                    return NotFound();

                SqlHelper.TransactionStarter(cmd =>
                {
                    RecordController.DeleteAll(cmd, SqlHelper.GetChildsOfType(cmd, LocateType.CONT_RECORDS, name));
                    NotificationController.DeleteAll(cmd, SqlHelper.GetChildsOfType(cmd, LocateType.CONT_NOTIFICATIONS, name));

                    cmd.CommandText = $"DELETE FROM {table} WHERE Name = @name";
                    cmd.Parameters.AddWithValue("@name", name);

                    cmd.ExecuteNonQuery();
                });

                NotificationController.ClearLeftoverNotifs();
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch
            {
                NotificationController.ClearLeftoverNotifs(false);
                return InternalServerError();
            }
        }

        public static void DeleteAll(SqlCommand cmd, List<string> names)
        {
            if (names.Count == 0)
                return;

            foreach (var name in names)
            {
                RecordController.DeleteAll(cmd, SqlHelper.GetChildsOfType(cmd, LocateType.CONT_RECORDS, name));
                NotificationController.DeleteAll(cmd, SqlHelper.GetChildsOfType(cmd, LocateType.CONT_NOTIFICATIONS, name));
            }

            string nameList = string.Join(",", names);

            cmd.CommandText = $"DELETE FROM {table} WHERE Name IN (@names)";
            cmd.Parameters.AddWithValue("@names", nameList);

            cmd.ExecuteNonQuery();
        }

        public static Container Find(string name, string parentName)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT t.* FROM {table} t" +
                    $" JOIN Applications a ON a.id = t.Application_Id" +
                    $" WHERE t.Name = @name AND a.Name = @parentName", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@parentName", parentName);

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

        public static bool NotExists(string name, string parentName)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table} t" +
                    $" JOIN Applications a ON a.id = t.Application_Id" +
                    $" WHERE t.Name = @name AND a.Name = @parentName", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@parentName", parentName);
                
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                return !reader.GetBoolean(0);
            });
        }
        public static bool Exists(string name)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}" +
                    $" WHERE Name = @name", conn);
                cmd.Parameters.AddWithValue("@name", name);
                
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                return reader.GetBoolean(0);
            });
        }
    }
}