namespace DatabaseHandler.Models;

public class DBConnModel
{
    //Connection String
    public string? ConnectionString { get; set; }
    //Database: 
    public string? DBName { get; set; }
    //Tables:
    public string? TableName { get; set; }
    public string? TableContent { get; set; }

}
