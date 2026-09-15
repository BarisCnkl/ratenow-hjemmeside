namespace RateNow.Web.Services;

/// <summary>
/// Virksomhedsoplysninger. Redigeres i appsettings.json under sektionen "Site" —
/// så du kan rette telefonnummer, e-mail og CVR uden at røre kode.
/// </summary>
public sealed class SiteSettings
{
    public const string SectionName = "Site";

    public string CompanyName { get; set; } = "RateNOW";
    public string LegalName { get; set; } = "RateNOW";
    public string Email { get; set; } = "kontakt@ratenow.dk";
    public string Phone { get; set; } = "+45 00 00 00 00";
    public string PhoneHref => "tel:" + new string(Phone.Where(c => char.IsDigit(c) || c == '+').ToArray());
    /// <summary>
    /// CVR-nummer. Står feltet tomt, vises det ingen steder på siden.
    /// Udfyld det for at få det med i privatlivspolitikken og handelsbetingelserne.
    /// </summary>
    public string Cvr { get; set; } = string.Empty;
    public string AddressLine { get; set; } = "Danmark";
    public string OpeningHours { get; set; } = "Man-fre 9-17";
    public string ResponsePromise { get; set; } = "Vi svarer normalt inden for én arbejdsdag.";

    /// <summary>Mappe hvor henvendelser gemmes som JSON, indtil du kobler e-mail på.</summary>
    public string ContactStoragePath { get; set; } = "App_Data/henvendelser";
}
