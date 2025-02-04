using _4CreateWebApiJsonUpload.Middleware;
using Appplication.Commands.Import;
using Appplication.Commands.Import.Get;
using Appplication.DTOs.Import.Post;
using _4CreateWebApiJsonUpload;
using CarStoreDatabaseAccess;
using Implementation.Commands.GetCommands;
using Implementation.Commands.ImportCommands;
using Microsoft.EntityFrameworkCore;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddLogging(logBuilder =>
        logBuilder.AddDebug()
            .AddConsole()
            .AddConfiguration(builder.Configuration.GetSection("Logging"))
            .SetMinimumLevel(LogLevel.Information));

    builder.Services.AddDbContext<MedicineContext>(x =>
    {
        var connectionString = builder.Configuration.GetValue<string>("LocalConnectionString");

        if (Environment.GetEnvironmentVariable("APP_HOST") != null && Environment.GetEnvironmentVariable("APP_HOST")?.ToString().ToLower() == "docker")
        {
            connectionString = builder.Configuration.GetValue<string>("DockerConnectionString");
        }

        x.UseSqlServer(connectionString, sqlBuilder =>
        {
            sqlBuilder.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            );
        });
    });

    builder.Services.SetupMapper();
    builder.Services.AddValidators();
    builder.Services.AddTransient<IJSonValidator, JsonValidator>();
    builder.Services.AddTransient<IReadJsonFileCommand<TrialDto>, ReadJsonFileCommand>();
    builder.Services.AddTransient<IImportJsonCommand, ImportJsonCommand>();
    builder.Services.AddTransient<IGetTrialCommand, GetTrialCommand>();
    builder.Services.AddTransient<ISearchTrialsCommand, SearchTrialsCommand>();

    var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseMiddleware<RequestCultureMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<MedicineContext>();

        context.Database.Migrate();      
    }

    app.Run();
}
catch(Exception e)
{
    Console.WriteLine(e);
}
public partial class Program { }
