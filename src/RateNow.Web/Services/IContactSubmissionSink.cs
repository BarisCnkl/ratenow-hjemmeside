using RateNow.Web.Models;

namespace RateNow.Web.Services;

/// <summary>
/// Modtager henvendelser fra kontaktformularen.
/// Skift implementeringen ud i Program.cs, når du vil have dem sendt på e-mail
/// eller ind i et CRM — resten af siden skal ikke ændres.
/// </summary>
public interface IContactSubmissionSink
{
    Task SubmitAsync(ContactRequest request, CancellationToken cancellationToken = default);
}
