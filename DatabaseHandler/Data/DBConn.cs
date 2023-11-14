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
        using (SqlConnection connection = new SqlConnection(_dbConnModel.ConnectionString))
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

        using (SqlConnection connection = new SqlConnection(_dbConnModel.ConnectionString))
        {
            connection.Open();
            string sql = $"SELECT * FROM sys.tables";
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

    public List<DBConnModel> ReadTable(DBConnModel dbConnModel)
    {
        List<DBConnModel> list = new();
        using (SqlConnection connection = new(_dbConnModel.ConnectionString))
        {
            connection.Open();
            string sql = $"SELECT * FROM {dbConnModel.DBName}";
            using (SqlCommand cmd = new(sql, connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.KeyInfo))
                {
                  while (reader.Read())
                    {
                        DBConnModel dBConnModelList = new()
                        {
                            TableName = reader.GetString(reader.GetOrdinal("name"))
                        };
                    }
                }
                connection.Close();
            }
        }
        return list;
    }    
}



