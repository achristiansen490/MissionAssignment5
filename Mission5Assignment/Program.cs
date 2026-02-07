// This lets Program.cs recognize the SmashBrosApiService class
using Mission5Assignment.Services;

// Creates a builder object that sets up the app configuration
var builder = WebApplication.CreateBuilder(args);

// -----------------------------
// Register services (Dependency Injection)
// -----------------------------

// Adds MVC support (Controllers + Views)
builder.Services.AddControllersWithViews();

// Registers our custom SmashBrosApiService
// This also automatically provides an HttpClient to it
builder.Services.AddHttpClient<SmashBrosApiService>();

// Build the application using the configured services
var app = builder.Build();

// -----------------------------
// Configure the HTTP request pipeline
// -----------------------------

// If we are NOT in development mode,
// use the production-style error handler
if (!app.Environment.IsDevelopment())
{
    // Sends users to /Home/Error if an exception occurs
    app.UseExceptionHandler("/Home/Error");

    // Enables HTTP Strict Transport Security
    app.UseHsts();
}

// Redirects all HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enables routing (figures out which controller/action to use)
app.UseRouting();

// Enables authorization middleware (used for login/roles)
app.UseAuthorization();

// Serves static files like CSS, JS, and images from wwwroot
app.UseStaticFiles();

// -----------------------------
// Define default route
// -----------------------------

// This sets the default page when someone visits the site
// Example: /Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Starts the web application
app.Run();