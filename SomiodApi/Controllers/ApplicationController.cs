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
    public class ApplicationController : ApiRoutes
    {
        private const string table = "Applications";

        private int LastId => SqlHelper.ConnectionStarter(conn =>
                {
                    SqlCommand cmd = new SqlCommand($"SELECT MAX(Id) FROM {table}", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                        return 0;

                    return (int)reader[0];
                });

        [HttpGet]
        [Route("{name}", Name = "GetApplication")]
        public IHttpActionResult Get(string name)
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
                var result = Find(name);

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
        public IEnumerable<string> GetAllLocate()
        {
            if (Request.Headers.Contains(specialHeader) && Request.Headers.GetValues(specialHeader).First() == "application")
            {
                return SqlHelper.GetChildsOfType(LocateType.APPLICATIONS, null);
            }

            return new List<string>();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] Application application)
        {
            try
            {
                if (!UrlNameValidation(application?.Name))
                    application.Name = (LastId + 1).ToString();
                if(application == null || Exists(application.Name))
                    application.Name = $"{application.Name}_{LastId + 1}";

                bool result = SqlHelper.ConnectionStarter(conn => {    
                    SqlCommand cmd = new SqlCommand($"INSERT INTO {table}(Name, Creation_Datetime) VALUES(@name, @creation_datetime)", conn);
                    cmd.Parameters.AddWithValue("@name", application.Name);
                    cmd.Parameters.AddWithValue("@creation_datetime", DateTime.Now);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                var app = Find(application.Name);
                return CreatedAtRoute("GetApplication", new { name = app.Name}, app);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("{name}")]
        public IHttpActionResult Update(string name, [FromBody] Application application)
        {
            try
            {
                Application oldApplication = Find(name);
                if (oldApplication == null)
                    return NotFound();

                if (application == null || !UrlNameValidation(application.Name))
                    return BadRequest();

                if (application.Name == null)
                    application.Name = $"{oldApplication.Id}";
                else if (Exists(application.Name))
                    BadRequest();
                
                bool result = SqlHelper.ConnectionStarter(conn => {    
                SqlCommand cmd = new SqlCommand($"UPDATE {table} SET Name = @name WHERE Name = @old_name", conn);
                    cmd.Parameters.AddWithValue("@name", application.Name);
                    cmd.Parameters.AddWithValue("@old_name", oldApplication.Name);

                    return cmd.ExecuteNonQuery() != 1;
                });

                if (result)
                    return BadRequest();

                return Ok(Find(application.Name));
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{name}")]
        public IHttpActionResult Delete(string name)
        {
            try
            {
                if (!Exists(name))
                    return NotFound();

                SqlHelper.TransactionStarter(cmd =>
                {
                    ContainerController.DeleteAll(cmd, SqlHelper.GetChildsOfType(LocateType.APP_CONTAINERS, name));

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

        public static Application Find(string name)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM {table} WHERE Name = @name", conn);
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

        public static bool Exists(string name)
        {
            return SqlHelper.ConnectionStarter(conn =>
            {
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table} WHERE Name = @name", conn);
                cmd.Parameters.AddWithValue("@name", name);
                
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                return reader.GetBoolean(0);
            });
        }
    }
}