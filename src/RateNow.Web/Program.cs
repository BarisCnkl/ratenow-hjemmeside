using RateNow.Web.Components;
using RateNow.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Virksomhedsoplysninger fra appsettings.json -> sektionen "Site".
builder.Services.Configure<SiteSettings>(
    builder.Configuration.GetSection(SiteSettings.SectionName));

// Henvendelser fra kontaktformularen. Vil du have dem på mail i stedet,
// så skriv en ny klasse der implementerer IContactSubmissionSink og skift linjen her.
builder.Services.AddSingleton<IContactSubmissionSink, FileContactSubmissionSink>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Ukendte adresser vises som vores egen 404-side — med 404 som statuskode.
app.UseStatusCodePagesWithReExecute("/ikke-fundet");

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Sitemap til søgemaskiner. Adressen tages fra den forespørgsel, der kommer ind,
// så den er rigtig både lokalt og på det rigtige domæne.
app.MapGet("/sitemap.xml", (HttpRequest request) =>
{
    string[] paths =
    [
        "/", "/produkter", "/saadan-virker-det", "/priser",
        "/om-os", "/faq", "/kontakt", "/privatlivspolitik", "/handelsbetingelser"
    ];

    var origin = $"{request.Scheme}://{request.Host}";
    var today = DateTime.UtcNow.ToString("yyyy-MM-dd");

    var urls = string.Concat(paths.Select(path =>
        $"<url><loc>{origin}{path}</loc><lastmod>{today}</lastmod></url>"));

    var xml = $"""<?xml version="1.0" encoding="UTF-8"?><urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">{urls}</urlset>""";

    return Results.Content(xml, "application/xml");
});

app.MapGet("/robots.txt", (HttpRequest request) =>
    Results.Text($"User-agent: *\nAllow: /\n\nSitemap: {request.Scheme}://{request.Host}/sitemap.xml\n"));

app.Run();
