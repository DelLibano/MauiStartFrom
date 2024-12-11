namespace MauiStartFrom.Data;

public static class Constants
{
    public const string Authorization = nameof(Authorization);

    public const string Weather = nameof(Weather);
    
    public const string Login = nameof(Login);
    
    public const string WebServiceName = nameof(WebServiceName);

    public const string WebServiceAddress = "http://localhost:5065/";

    public const string DatabaseFilename = "AppSQLite.db3";

    public static string DatabasePath => $"Data Source={Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename)}";
}