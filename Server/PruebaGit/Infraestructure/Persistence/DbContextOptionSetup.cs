using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace PruebaRaddarStudios.Infraestructure.Persistence;

public static class DbContextOptionSetup
{
    public static void ConfigureReadOptions(DbContextOptionsBuilder options, string connectionString)
    {
        options
            .UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(30);
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            })
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .EnableSensitiveDataLogging(false)
            .EnableDetailedErrors(false)
            .ConfigureWarnings(warnings =>
                warnings.Ignore(CoreEventId.MultipleNavigationProperties));
    }

    public static void ConfigureWriteOptions(DbContextOptionsBuilder options, string connectionString)
    {
        options
            .UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
                sqlOptions.CommandTimeout(90);
                sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            })
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .EnableSensitiveDataLogging(false)
            .EnableDetailedErrors(false);
    }
}