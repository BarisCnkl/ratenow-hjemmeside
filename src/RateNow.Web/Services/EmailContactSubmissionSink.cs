using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RateNow.Web.Models;

namespace RateNow.Web.Services;

/// <summary>
/// Gemmer først henvendelsen som fil (så intet går tabt) og sender den derefter på mail.
/// Svarer du på mailen, går svaret direkte til kunden.
/// Er mailserveren ikke sat op, gemmes henvendelsen kun som fil.
/// </summary>
public sealed class EmailContactSubmissionSink : IContactSubmissionSink
{
    private readonly FileContactSubmissionSink _file;
    private readonly SmtpSettings _smtp;
    private readonly string _companyName;
    private readonly ILogger<EmailContactSubmissionSink> _logger;

    public EmailContactSubmissionSink(
        FileContactSubmissionSink file,
        IOptions<SmtpSettings> smtp,
        IOptions<SiteSettings> site,
        ILogger<EmailContactSubmissionSink> logger)
    {
        _file = file;
        _smtp = smtp.Value;
        _companyName = site.Value.CompanyName;
        _logger = logger;
    }

    public async Task SubmitAsync(ContactRequest request, CancellationToken cancellationToken = default)
    {
        await _file.SubmitAsync(request, cancellationToken);

        if (!_smtp.IsConfigured)
        {
            _logger.LogWarning("Mail er ikke sat op (mangler Smtp__Password). Henvendelsen er kun gemt som fil.");
            return;
        }

        // Fejler afsendelsen, kastes fejlen videre, så kunden får besked om at skrive direkte.
        // Henvendelsen er allerede gemt som fil.
        await SendAsync(request, cancellationToken);
    }

    private async Task SendAsync(ContactRequest request, CancellationToken cancellationToken)
    {
        var from = string.IsNullOrWhiteSpace(_smtp.From) ? _smtp.Username : _smtp.From;

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress($"{_companyName} hjemmeside", from));
        message.To.Add(MailboxAddress.Parse(_smtp.To));
        message.ReplyTo.Add(new MailboxAddress(request.Name, request.Email));
        message.Subject = $"Ny henvendelse fra {request.Company}";
        message.Body = new TextPart("plain") { Text = BuildBody(request) };

        using var client = new SmtpClient();
        await client.ConnectAsync(_smtp.Host, _smtp.Port, SecureSocketOptions.StartTlsWhenAvailable, cancellationToken);
        await client.AuthenticateAsync(_smtp.Username, _smtp.Password, cancellationToken);
        await client.SendAsync(message, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);

        _logger.LogInformation("Henvendelse fra {Company} sendt på mail", request.Company);
    }

    private static string BuildBody(ContactRequest request)
    {
        var body = new StringBuilder()
            .AppendLine("Ny henvendelse fra kontaktformularen på hjemmesiden.")
            .AppendLine()
            .AppendLine($"Navn:           {request.Name}")
            .AppendLine($"Virksomhed:     {request.Company}")
            .AppendLine($"E-mail:         {request.Email}")
            .AppendLine($"Telefon:        {OrDash(request.Phone)}")
            .AppendLine($"Interesseret i: {OrDash(request.Interest)}")
            .AppendLine()
            .AppendLine("Besked:")
            .AppendLine(request.Message)
            .AppendLine()
            .AppendLine("Tryk svar for at skrive direkte til kunden.");

        return body.ToString();
    }

    private static string OrDash(string? value) => string.IsNullOrWhiteSpace(value) ? "-" : value;
}
