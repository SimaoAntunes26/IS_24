using SOMIOD.Common;
using SOMIOD.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace SOMIOD.Controllers
{
    public partial class SomiodController : ApiRoutes
    {
        private const string recordTable = "Records";
        private int RecordLastId => SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"SELECT IDENT_CURRENT('{recordTable}')", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read() || reader[0] == DBNull.Value)
                        return 0;

                    return Convert.ToInt32(reader[0]);
                });

        [HttpGet]
        [Route("{applicationName}/{containerName}/record/{name}", Name = "GetRecord")]
        public IHttpActionResult GetRecord(string applicationName, string containerName, string name)
        {
            try
            {
                var result = FindRecord(name, containerName, applicationName);
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
        public IHttpActionResult CreateRecord(string applicationName, string containerName, [FromBody] Record record)
        {
            if (record == null || record?.Content == null)
                return BadRequest("Body needs to contain at least record content");

            try
            {
                Container container = FindContainer(containerName, applicationName);
                if (container == null)
                    return NotFound();

                string name = record?.Name;
                if (record == null || !UrlNameValidation(name))
                    name = (RecordLastId + 1).ToString();
                if (ExistsRecord(name))
                    name = $"{name}_{RecordLastId + 1}";

                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO {recordTable}(Name, Creation_Datetime, Container_Id, Content) VALUES(@name, @creation_datetime, @parent, @content)", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@creation_datetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@parent", container.Id);
                    cmd.Parameters.AddWithValue("@content", record.Content);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                var rec = FindRecord(name, container.Name, applicationName);
                return CreatedAtRoute("GetRecord", new { name = rec.Name }, rec);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{applicationName}/{containerName}/record/{name}")]
        public IHttpActionResult DeleteRecord(string applicationName, string containerName, string name)
        {
            try
            {
                if (NotExistsRecord(name, containerName, applicationName))
                    return NotFound();

                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"DELETE FROM {recordTable} WHERE Name = @name", conn);
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

        public static void DeleteAllRecords(SqlCommand cmd, List<string> names)
        {
            if (names.Count == 0)
                return;

            string nameList = string.Join(",", names);

            cmd.CommandText = $"DELETE FROM {recordTable} WHERE Name IN (@names)";
            cmd.Parameters.AddWithValue("@names", nameList);

            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
        }

        public static Record FindRecord(string name, string parentName, string grandparentName)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT t.* FROM {recordTable} t" +
                    $" JOIN Containers c ON c.id = t.Container_Id " +
                    $" JOIN Applications a ON a.id = c.Application_Id" +
                    $" WHERE t.Name = @name AND c.Name = @parent_name AND a.Name = @grandparent_name", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@parent_name", parentName);
                cmd.Parameters.AddWithValue("@grandparent_name", grandparentName);

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

        public static bool NotExistsRecord(string name, string parentName, string grandParentName)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {recordTable} t" +
                    $" JOIN Containers c ON c.id = t.Container_Id" +
                    $" JOIN Applications a ON a.id = c.Application_Id" +
                    $" WHERE t.Name = @name AND c.Name = @parent_name AND a.Name = @grand_parent_name", conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@parent_name", parentName);
                cmd.Parameters.AddWithValue("@grand_parent_name", grandParentName);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                return (int)reader[0] == 0;
            });
        }
        public static bool ExistsRecord(string name)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {recordTable}" +
                    $" WHERE Name = @name", conn);
                cmd.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                return (int)reader[0] > 0;
            });
        }
    }
}