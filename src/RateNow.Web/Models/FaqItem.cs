namespace RateNow.Web.Models;

public sealed record FaqItem(string Question, string Answer, string Category = "Generelt");
