using DatabaseHandler.Models;
using System.Data.SqlClient;


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
                string sql = $"CREATE DATABASE {_dbConnModel.TableName}";
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
                        DBConnModel dbTableModel = new()
                        {
                            TableName = reader.GetString(reader.GetOrdinal("name"))
                        };
                        list.Add(dbTableModel);
                    }
                }
            }
            connection.Close();
        }
        return list;
    }

    public List<DBConnModel> GetAllRecords(List<DBConnModel> dbConnList)
    {
        List<DBConnModel> records = new();
        using (SqlConnection connection = new SqlConnection(_dbConnModel.ConnectionString))
        {
            connection.Open();
            foreach (var item in dbConnList)
            {
                string sql = $"SELECT * FROM {item.TableName}";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DBConnModel record = new DBConnModel();
                        record.TableName = item.TableName;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string ColumnName = reader.GetName(i);
                            object RowValue = reader.GetValue(i);

                            record.ColumnName.Add(ColumnName);
                            record.RowValue.Add(RowValue);
                        }
                        records.Add(record);
                    }
                }
            }
            return records;
        }
    }
}
