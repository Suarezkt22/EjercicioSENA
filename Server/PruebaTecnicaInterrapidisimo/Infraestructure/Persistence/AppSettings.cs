namespace PruebaTecnicaInterrapidisimo.Infraestructure.Persistence;

public class AppSettings
{
    public const string SectionKey = "ConnectionString";
    public string DbConnectionString { get; set; } = string.Empty;
}
