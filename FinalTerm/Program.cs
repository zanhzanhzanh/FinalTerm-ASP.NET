global using FinalTerm.Database;
global using Microsoft.EntityFrameworkCore;
using FinalTerm.Startup;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        builder.Services.ConfigureAppSettings(configuration);

        var app = builder.Build();
        app.ConfigureMiddleware();
        app.Run();
    }
}