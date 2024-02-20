var builder = WebApplication.CreateBuilder(args);

// Add framework services.
builder.Services
	.AddRazorPages().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Add Kendo UI services to the services container
builder.Services.AddKendo();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/Home"));


app.MapRazorPages();

app.Run();
