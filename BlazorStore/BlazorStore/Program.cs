using Blazored.LocalStorage;
using BlazorStore.Components;
using BlazorStore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents()
	.AddInteractiveWebAssemblyComponents();

/*builder.Services.AddDbContext<ProductContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDb"))
);*/
var connectionString = builder.Configuration.GetConnectionString("ProductDb");

builder.Services.AddDbContextFactory<ProductContext>(options => options.UseSqlite(connectionString));
builder.Services.AddScoped<DataAccess>();
//builder.Services.AddScoped<ProductContext>();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode()
	.AddInteractiveWebAssemblyRenderMode()
	.AddAdditionalAssemblies(typeof(BlazorStore.Client._Imports).Assembly);

app.Run();
