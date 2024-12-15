using SomiodApi.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace SOMIOD.Common
{
    public static class SqlHelper
    {
        private static string DBConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + "App_Data\\SOMIOD_DB.mdf;Integrated Security=True";

        public static List<string> GetChildsOfType(SqlCommand cmd, LocateType type, string parentName, string grandparentName)
        {
            List<string> names = new List<string>();

            switch (type)
            {
                case LocateType.APPLICATIONS:
                    cmd.CommandText = "SELECT Name FROM Applications";
                    break;
                case LocateType.APP_CONTAINERS:
                    cmd.CommandText = "SELECT c.Name FROM Containers c " +
                        "JOIN Applications a ON a.Id = c.Application_Id " +
                        "WHERE a.Name = @NAME";
                    cmd.Parameters.AddWithValue("@NAME", parentName);
                    break;
                case LocateType.CONT_RECORDS:
                    cmd.CommandText = "SELECT r.Name FROM Records r " +
                        "JOIN Containers c ON c.id = r.Container_Id " +
                        "JOIN Applications a ON a.id = c.Application_Id " +
                        "WHERE c.Name = @NAME AND a.Name = @GRANDNAME";
                    cmd.Parameters.AddWithValue("@NAME", parentName);
                    cmd.Parameters.AddWithValue("@GRANDNAME", grandparentName);
                    break;
                case LocateType.CONT_NOTIFICATIONS:
                    cmd.CommandText = "SELECT n.Name FROM Notifications n " +
                        "JOIN Containers c ON c.id = n.Container_Id " +
                        "JOIN Applications a ON a.id = c.Application_Id " +
                        "WHERE c.Name = @NAME AND a.Name = @GRANDNAME";
                    cmd.Parameters.AddWithValue("@NAME", parentName);
                    cmd.Parameters.AddWithValue("@GRANDNAME", grandparentName);
                    break;
                case LocateType.APP_RECORDS:
                    cmd.CommandText = "SELECT r.Name FROM Records r " +
                        "JOIN Containers c ON c.id = r.Container_Id " +
                        "JOIN Applications a ON a.id = c.Application_Id " +
                        "WHERE a.Name = @NAME";
                    cmd.Parameters.AddWithValue("@NAME", parentName);
                    break;
                case LocateType.APP_NOTIFICATIONS:
                    cmd.CommandText = "SELECT n.Name FROM Notifications n " +
                        "JOIN Containers c ON c.id = n.Container_Id " +
                        "JOIN Applications a ON a.id = c.Application_Id " +
                        "WHERE a.Name = @NAME";
                    cmd.Parameters.AddWithValue("@NAME", parentName);
                    break;
                default:
                    throw new Exception("Type not recognized");
            }

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                names.Add((string)reader[0]);
            }

            reader.Close();
            cmd.Parameters.Clear();

            return names;
        }
        public static List<string> GetChildsOfType(LocateType type, string parentName, string grandparentName = null)
        {
            return ConnectionStarter(conn =>
            {
                return GetChildsOfType(conn.CreateCommand(), type, parentName, grandparentName);
            });
        }

        public static TResult ConnectionStarter<TResult>(Func<SqlConnection, TResult> operation)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(DBConnection);
                conn.Open();

                return operation(conn);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public static void TransactionStarter(Action<SqlCommand> operation)
        {
            ConnectionStarter(conn =>
            {
                SqlTransaction transaction = null;

                try
                {
                    transaction = conn.BeginTransaction();
                    
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;
                    
                    operation(cmd);

                    transaction.Commit();

                    return true;
                }
                catch
                {
                    transaction?.Rollback();
                    throw;
                }
            });
        }
    }
}