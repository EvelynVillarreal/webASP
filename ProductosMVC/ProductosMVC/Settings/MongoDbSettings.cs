namespace ProductosMVC.Settings;

public sealed class MongoDbSettings
{
    public const string ConfigurationSection = "MongoDbSettings";

    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}
