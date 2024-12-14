using SOMIOD.Common;
using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SOMIOD.Controllers
{
    [RoutePrefix("api/somiod")]
    public class RecordController : ApiRoutes
    {
        private const string table = "Records";
        private int LastId => SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"SELECT MAX(Id) FROM {table}", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                        return 0;

                    return (int)reader[0];
                });

        [HttpGet]
        [Route("{applicationName}/{containerName}/record/{name}", Name = "GetRecord")]
        public IHttpActionResult Get(string applicationName, string containerName, string name)
        {
            try
            {
                var result = Find(name, containerName, applicationName);
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
        [Route("{applicationName}/{containerName}/record")]
        public IHttpActionResult Create(string applicationName, string containerName, [FromBody] Record record)
        {
            if (record == null)
                return BadRequest("Body needs to contain at least record content");
            
            try
            {
                Container container = ContainerController.Find(containerName, applicationName);
                if (container == null)
                    return NotFound();

                if (!UrlNameValidation(record.Name))
                    record.Name = (LastId + 1).ToString();
                else if (Exists(record.Name))
                    record.Name = $"{record.Name}_{LastId + 1}";

                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO {table}(Name, Creation_Datetime, Container_Id, Content) VALUES(@name, @creation_datetime, @parent, @content)", conn);
                    cmd.Parameters.AddWithValue("@name", record.Name);
                    cmd.Parameters.AddWithValue("@creation_datetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@parent", container.Id);
                    cmd.Parameters.AddWithValue("@content", record.Content);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                var rec = Find(record.Name, container.Name, applicationName);
                return CreatedAtRoute("GetRecord", new { name = rec.Name}, rec);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{applicationName}/{containerName}/record/{name}")]
        /**
         * Might rework this for a recursive delete
         */
        public IHttpActionResult Delete(string applicationName, string containerName, string name)
        {
            try
            {
                if (NotExists(name, containerName, applicationName))
                    return NotFound();

                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM {table} WHERE Name = @name", conn);
                    cmd.Parameters.AddWithValue("@name", name);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            catch
            {
                return InternalServerError();
            }
        }

        public static void DeleteAll(SqlCommand cmd, List<string> names)
        {
            if (names.Count == 0)
                return;

            string nameList = string.Join(",", names);

            cmd.CommandText = $"DELETE FROM {table} WHERE Name IN (@names)";
            cmd.Parameters.AddWithValue("@names", nameList);

            cmd.ExecuteNonQuery();
        }

        public static Record Find(string name, string parentName, string grandParentName)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT t.* FROM {table} t" +
                    $" JOIN Containers c ON c.id = t.Container_Id " +
                    $" JOIN Applications a ON a.id = c.Application_Id" +
                    $" WHERE t.Name = @name AND a.Name = @parentName", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@parentName", parentName);

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                    return null;

                Record record = new Record
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    CreationDatetime = (DateTime)reader["Creation_Datetime"],
                    ParentId = (int)reader["Container_Id"],
                    Content = (string)reader["Content"]
                };

                return record;
            });
        }

        public static bool NotExists(string name, string parentName, string grandParentName)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table} t" +
                    $" JOIN Containers c ON c.id = t.Containers_Id" +
                    $" JOIN Applications a ON a.id = c.Application_Id" +
                    $" WHERE t.Name = @name AND c.Name = @parentName AND a.Name = @grandParentName", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@parentName", parentName);
                cmd.Parameters.AddWithValue("@grandParentName", grandParentName);
                
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