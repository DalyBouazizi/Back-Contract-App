using Microsoft.EntityFrameworkCore;
using Projet_Data.RepoInjection;
using Projet_Data.ModelsEF;
using Projet_Stage.ServiceInjection;


var builder = WebApplication.CreateBuilder(args);

// Configure JSON options with custom DateOnly converter
builder.Services.AddControllers();
    

// Register repository services and custom services
builder.Services.AddRepositoryServices();
builder.Services.InjectServices();

// Configure database context
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Projet_Data")));

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting(); // Ensure this is added before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
