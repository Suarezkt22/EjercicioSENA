using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GitEjercicioSENA.Infraestructure.Persistence;

public static class DbContextOptionSetup
{
    public static void ConfigureReadOptions(DbContextOptionsBuilder options, string connectionString)
    {
        options
            .UseSqlite("Data Source=database.db")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .EnableSensitiveDataLogging(false)
            .EnableDetailedErrors(false)
            .ConfigureWarnings(warnings =>
                warnings.Ignore(CoreEventId.MultipleNavigationProperties));
    }

    public static void ConfigureWriteOptions(DbContextOptionsBuilder options, string connectionString)
    {
        options
            .UseSqlite("Data Source=database.db")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging(false)
            .EnableDetailedErrors(false);
    }
}