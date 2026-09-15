namespace RateNow.Web.Models;

/// <summary>
/// Et fysisk produkt i RateNOW-sortimentet. Indholdet redigeres i <see cref="Services.SiteContent"/>.
/// </summary>
public sealed record Product(
    string Slug,
    string Name,
    string Tagline,
    string Description,
    string PriceLabel,
    string[] Highlights,
    string BestFor,
    string Icon,
    bool IsPopular = false,
    /// <summary>Lille tekst under prisen, f.eks. "pr. stk.". Tom hvis den ikke skal vises.</summary>
    string PriceNote = "");
