using Microsoft.AspNetCore.Mvc.Routing;
using OrganizationChartMIS.Data.DatabaseHelper;

var builder = WebApplication.CreateBuilder(args);

// Add framework services.
builder.Services
	.AddRazorPages().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Add Kendo UI services to the services container
builder.Services.AddKendo();

builder.Services.AddTransient<DatabaseHelper>(serviceProvider =>
{
    return new DatabaseHelper(builder.Configuration);
});

// Add services to the container.

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

var connectionString = builder.Configuration.GetConnectionString("OrgMISConnection")!;
//builder.Services.AddSingleton(new DatabaseHelper(connectionString));






app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/Home"));


app.MapRazorPages();

app.Run();
