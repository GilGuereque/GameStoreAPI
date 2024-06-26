using GameStore.Frontend.Components;
using GameStore.Frontend.Clients;

// Create our application host which is WebApplication
var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (using dependency injection)
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

var gameStoreApiUrl = builder.Configuration["GameStoreApiUrl"] ??
    throw new Exception("GameStoreApiUrl is not set");

builder.Services.AddHttpClient<GamesClient>(
    client => client.BaseAddress = new Uri(gameStoreApiUrl));

builder.Services.AddHttpClient<GenresClient>(
    client => client.BaseAddress = new Uri(gameStoreApiUrl));

var app = builder.Build();

// Middleware
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) //if we are not in dev, so production
{
    // Use UseExceptionHandler to send an Error to the user if something goes wrong in the app
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Set application up to use HTTPS if needed (we won't be using this)
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Configure middleware to be able to discover all the Razor components we created for our app
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Lastly invoke Run to start our application and enable it to start receiving requests
app.Run();
