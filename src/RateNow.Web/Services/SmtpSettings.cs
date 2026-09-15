namespace RateNow.Web.Services;

/// <summary>
/// Opsætning af mailserveren, der sender henvendelser videre. Redigeres i appsettings.json
/// under sektionen "Smtp". Adgangskoden skal IKKE stå i filen — den sættes på serveren
/// som indstillingen <c>Smtp__Password</c>. Uden adgangskode sendes der ingen mails.
/// </summary>
public sealed class SmtpSettings
{
    public const string SectionName = "Smtp";

    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    /// <summary>Afsender. Hos Gmail skal det være den samme adresse som <see cref="Username"/>.</summary>
    public string From { get; set; } = string.Empty;

    /// <summary>Hvor henvendelserne skal sendes hen.</summary>
    public string To { get; set; } = string.Empty;

    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(Host) &&
        !string.IsNullOrWhiteSpace(Username) &&
        !string.IsNullOrWhiteSpace(Password) &&
        !string.IsNullOrWhiteSpace(To);
}
