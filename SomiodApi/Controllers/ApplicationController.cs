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
    [RoutePrefix("api/somiod")]
    public partial class SomiodController : ApiRoutes
    {
        private const string applicationsTable = "Applications";

        private int ApplicationLastId => SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"SELECT IDENT_CURRENT('{applicationsTable}')", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read() || reader[0] == DBNull.Value)
                        return 0;

                    return Convert.ToInt32(reader[0]);
                });

        [HttpGet]
        [Route("{name}", Name = "GetApplication")]
        public IHttpActionResult GetApplication(string name)
        {
            if (Request.Headers.Contains(specialHeader))
                switch (Request.Headers.GetValues(specialHeader).First())
                {
                    case "container":
                        return Ok(SqlHelper.GetChildsOfType(LocateType.APP_CONTAINERS, name));
                    case "record":
                        return Ok(SqlHelper.GetChildsOfType(LocateType.APP_RECORDS, name));
                    case "notification":
                        return Ok(SqlHelper.GetChildsOfType(LocateType.APP_NOTIFICATIONS, name));
                    default:
                        return BadRequest("Locator unknown");
                }

            try
            {
                var result = FindApplication(name);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<string> GetAllApplicationLocate()
        {
            if (Request.Headers.Contains(specialHeader))
            {
                switch (Request.Headers.GetValues(specialHeader).First())
                {
                    case "application":
                        return SqlHelper.GetChildsOfType(LocateType.APPLICATIONS, null);
                    case "container":
                        return SqlHelper.GetChildsOfType(LocateType.CONTAINERS, null);
                    case "record":
                        return SqlHelper.GetChildsOfType(LocateType.RECORDS, null);
                    case "notification":
                        return SqlHelper.GetChildsOfType(LocateType.NOTIFICATIONS, null);
                }
                
            }

            return new List<string>();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateApplication([FromBody] Application application)
        {
            try
            {
                string name = application?.Name;
                if (application == null || !UrlNameValidation(name))
                    name = (ApplicationLastId + 1).ToString();
                if (ExistsApplication(name))
                    name = $"{name}_{ApplicationLastId + 1}";

                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"INSERT INTO {applicationsTable}(Name, Creation_Datetime) VALUES(@name, @creation_datetime)", conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@creation_datetime", DateTime.Now);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                var app = FindApplication(name);
                return CreatedAtRoute("GetApplication", new { name = app.Name }, app);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("{name}")]
        public IHttpActionResult UpdateApplication(string name, [FromBody] Application application)
        {
            try
            {
                Application oldApplication = FindApplication(name);
                if (oldApplication == null)
                    return NotFound();

                if (!UrlNameValidation(application?.Name))
                    return BadRequest();

                string newName = application?.Name;
                if (newName == null)
                    newName = $"{oldApplication.Id}";
                else if (oldApplication.Name == newName || ExistsApplication(newName))
                    BadRequest("Name is the same or already exists");

                bool result = SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"UPDATE {applicationsTable} SET Name = @name WHERE Name = @old_name", conn);
                    cmd.Parameters.AddWithValue("@name", newName);
                    cmd.Parameters.AddWithValue("@old_name", oldApplication.Name);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                return Ok(FindApplication(newName));
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{name}")]
        public IHttpActionResult DeleteApplication(string name)
        {
            try
            {
                if (!ExistsApplication(name))
                    return NotFound();

                SqlHelper.TransactionStarter(cmd =>
                {
                    DeleteAllContainers(cmd, SqlHelper.GetChildsOfType(LocateType.APP_CONTAINERS, name), name);

                    cmd.CommandText = $"DELETE FROM {applicationsTable} WHERE Name = @name";
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

        public static Application FindApplication(string name)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM {applicationsTable} WHERE Name = @name", conn);
                cmd.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                    return null;

                Application app = new Application
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    CreationDatetime = (DateTime)reader["Creation_Datetime"]
                };

                return app;
            });
        }

        public static bool ExistsApplication(string name)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {applicationsTable} WHERE Name = @name", conn);
                cmd.Parameters.AddWithValue("@name", name);

                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                return (int)reader[0] > 0;
            });
        }
    }
}