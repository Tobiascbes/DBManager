namespace DatabaseHandler.Models;

public class DBConnModel
{
    //Connection String
    public string? ConnectionString { get; set; }
    //Database: 
    public string? TableName { get; set; }
    //Tables:
    public List<string> ColumnName { get; set;} = new();
    public List<object> RowValue { get; set; } = new();
}
