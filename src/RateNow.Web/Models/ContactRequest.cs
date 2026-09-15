using System.ComponentModel.DataAnnotations;

namespace RateNow.Web.Models;

/// <summary>
/// Henvendelse fra kontaktformularen. Valideringsbeskeder er på dansk, da de vises direkte i UI.
/// </summary>
public sealed class ContactRequest
{
    [Required(ErrorMessage = "Skriv venligst dit navn.")]
    [StringLength(80, ErrorMessage = "Navnet må højst være 80 tegn.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Skriv venligst virksomhedens navn.")]
    [StringLength(120, ErrorMessage = "Virksomhedsnavnet må højst være 120 tegn.")]
    public string Company { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vi har brug for en e-mail for at kunne svare dig.")]
    [EmailAddress(ErrorMessage = "E-mailen ser ikke rigtig ud.")]
    [StringLength(160)]
    public string Email { get; set; } = string.Empty;

    // Feltet er valgfrit, så et tomt felt gemmes som null.
    // Ellers ville [Phone] afvise den tomme streng, som browseren sender.
    [Phone(ErrorMessage = "Telefonnummeret ser ikke rigtigt ud.")]
    [StringLength(40)]
    public string? Phone
    {
        get => _phone;
        set => _phone = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
    private string? _phone;

    [StringLength(60)]
    public string? Interest { get; set; }

    [Required(ErrorMessage = "Skriv et par ord om, hvad vi kan hjælpe med.")]
    [StringLength(2000, MinimumLength = 5, ErrorMessage = "Beskeden skal være mellem 5 og 2000 tegn.")]
    public string Message { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true", ErrorMessage = "Du skal acceptere, at vi må kontakte dig.")]
    public bool ConsentGiven { get; set; }

    /// <summary>Honeypot: udfyldes kun af bots, og feltet er skjult for mennesker.</summary>
    public string? Website { get; set; }
}
