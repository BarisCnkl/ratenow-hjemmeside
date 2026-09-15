using System.Text.Json;
using Microsoft.Extensions.Options;
using RateNow.Web.Models;

namespace RateNow.Web.Services;

/// <summary>
/// Gemmer hver henvendelse som en JSON-fil på disken og skriver en linje i loggen.
/// Det betyder, at formularen virker fra dag ét — også før der er sat en mailserver op.
/// </summary>
public sealed class FileContactSubmissionSink : IContactSubmissionSink
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    private readonly string _folder;
    private readonly ILogger<FileContactSubmissionSink> _logger;

    public FileContactSubmissionSink(
        IOptions<SiteSettings> settings,
        IWebHostEnvironment environment,
        ILogger<FileContactSubmissionSink> logger)
    {
        _logger = logger;
        _folder = Path.Combine(environment.ContentRootPath, settings.Value.ContactStoragePath);
    }

    public async Task SubmitAsync(ContactRequest request, CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_folder);

        var stamp = DateTimeOffset.Now;
        var fileName = $"{stamp:yyyy-MM-dd_HHmmss}_{Sanitize(request.Company)}.json";
        var path = Path.Combine(_folder, fileName);

        var payload = new
        {
            ModtagetTidspunkt = stamp,
            request.Name,
            request.Company,
            request.Email,
            request.Phone,
            request.Interest,
            request.Message
        };

        await File.WriteAllTextAsync(path, JsonSerializer.Serialize(payload, JsonOptions), cancellationToken);

        _logger.LogInformation("Ny henvendelse fra {Company} gemt i {File}", request.Company, fileName);
    }

    private static string Sanitize(string value)
    {
        var cleaned = new string(value
            .Where(c => char.IsLetterOrDigit(c) || c is '-' or '_')
            .Take(40)
            .ToArray());

        return string.IsNullOrWhiteSpace(cleaned) ? "henvendelse" : cleaned;
    }
}
