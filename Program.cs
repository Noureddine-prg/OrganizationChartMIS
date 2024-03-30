using OrganizationChartMIS.Data.Context;

using OrganizationChartMIS.Data.Repositories.Department;
using OrganizationChartMIS.Data.Repositories.Employee;
using OrganizationChartMIS.Data.Repositories.Position;
using OrganizationChartMIS.Data.Repositories.Team;
using OrganizationChartMIS.Data.Service.Department;

var builder = WebApplication.CreateBuilder(args);

// Add framework services.
builder.Services
    .AddRazorPages()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Add Kendo UI services to the services container
builder.Services.AddKendo();

// Add scoped services
builder.Services.AddScoped<PositionRepository>();
builder.Services.AddScoped<EmployeeRepository>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<TeamRepository>();


builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPositionService, PositionService>();


builder.Services.AddScoped<DatabaseHelper>(serviceProvider => {
    return new DatabaseHelper(builder.Configuration);
});

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
/*
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
*/

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Endpoints
app.MapGet("/", () => Results.Redirect("/Home"));
app.MapRazorPages();

// Start the application
app.Run();
