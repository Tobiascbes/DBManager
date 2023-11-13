using DatabaseHandler.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;


namespace DatabaseHandler.Data;

public class DBConn
{
    private DBConnModel _dbConnModel;
    public DBConn(DBConnModel dbConnModel)
    {
        _dbConnModel = dbConnModel;
    }

    public void Create()
    {
        using (SqlConnection connection = new SqlConnection(DBConnModel.ConnectionString))
        {
            connection.Open();
            Console.WriteLine("Connection Opened Successfully");
            {
                connection.Open();

                string sql = $"CREATE DATABASE {_dbConnModel.DBName}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Database Created Successfully");
                }
                connection.Close();
            }
        }
    }

    public List<DBConnModel> Read()
    {
        List<DBConnModel> list = new();

        using (SqlConnection connection = new SqlConnection(DBConnModel.ConnectionString))
        {
            connection.Open();
            string sql = $"SELECT * FROM sys.databases";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DBConnModel dbConnModel = new()
                        {
                            DBName = reader.GetString(reader.GetOrdinal("name"))
                        };
                        list.Add(dbConnModel);
                    }
                   
                }
                connection.Close();
            }
            return list;
        }

    }
        
    
}



