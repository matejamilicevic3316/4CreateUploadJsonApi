using _4CreateWebApiJsonUpload.Middleware;
using Appplication.Commands.Import;
using Appplication.Commands.Import.Get;
using Appplication.DTOs.Import.Post;
using CarStore.Api;
using CarStoreDatabaseAccess;
using Implementation.Commands.GetCommands;
using Implementation.Commands.ImportCommands;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MedicineContext>(x =>
{
    x.UseSqlServer(@"Data Source=DESKTOP-SK56QU7;Initial Catalog=4CreateDatabase;Integrated Security=True;TrustServerCertificate=True");
});
builder.Services.SetupMapper();
builder.Services.AddValidators();
builder.Services.AddTransient<IJSonValidator,JsonValidator>();
builder.Services.AddTransient<IReadJsonFileCommand<TrialDto>, ReadJsonFileCommand>();
builder.Services.AddTransient<IImportJsonCommand, ImportJsonCommand>();
builder.Services.AddTransient<IGetTrialCommand, GetTrialCommand>();
builder.Services.AddTransient<ISearchTrialsCommand, SearchTrialsCommand>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMiddleware<RequestCultureMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
