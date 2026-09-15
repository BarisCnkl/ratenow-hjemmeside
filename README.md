# RateNOW — hjemmeside

Marketingside bygget i .NET 9 med Blazor (C#). Siden bruger **static server rendering**,
så den er hurtig, virker uden JavaScript og kan læses af Google.

## Kør den lokalt

```bash
dotnet run --project src/RateNow.Web
```

Åbn derefter <http://localhost:5139>.

Vil du have siden til at opdatere sig selv, mens du retter i filerne:

```bash
dotnet watch --project src/RateNow.Web
```

## Det, du skal rette først

| Hvad | Hvor |
| --- | --- |
| E-mail, telefon, CVR, adresse, åbningstider | `src/RateNow.Web/appsettings.json` → sektionen `Site` (tomt telefon- eller CVR-felt skjules på siden) |
| Priser | `src/RateNow.Web/Services/SiteContent.cs` → `Products` |
| Produktnavne og beskrivelser | samme fil, `Products` |
| Spørgsmål og svar | samme fil, `Faq` |
| De fire trin under "Sådan virker det" | samme fil, `Steps` |
| Historien på "Om os" | `src/RateNow.Web/Components/Pages/OmOs.razor` |
| Privatlivspolitik og handelsbetingelser | `Components/Pages/Privatlivspolitik.razor` og `Handelsbetingelser.razor` |

De to juridiske sider er **udkast**. Få dem læst igennem, før siden går live — særligt
afsnittene om levering, betaling og returret, som skal passe til, hvordan I faktisk gør.

## Sådan er det skruet sammen

```
src/RateNow.Web/
  Components/
    Layout/      SiteHeader, SiteFooter, MainLayout
    Pages/       En fil pr. side (Home, Produkter, Priser, Kontakt …)
    Shared/      Genbrugsdele: Icon, Logo, ProductCard, ProductArt,
                 TapAnimation, FaqList, CtaBand …
  Models/        ContactRequest, Product, FaqItem, Step
  Services/      SiteContent (alt tekstindhold), SiteSettings, kontaktformularens modtager
  wwwroot/
    app.css      Hele designsystemet. Farver ligger som variabler øverst i :root
    js/site.js   Mobilmenu og skygge under headeren
```

### Farver og udseende

Alt visuelt styres af variablerne øverst i `wwwroot/app.css`. Skal brandfarven ændres,
retter du `--brand` ét sted, og hele siden følger med.

### Produktbilleder og animation

Produktbillederne er tegnet i kode i `Components/Shared/ProductArt.razor`, én
variant pr. produkt valgt ud fra produktets `Slug`. Vil du hellere bruge rigtige
fotos, så læg dem i `wwwroot/img/produkter/` og erstat `<ProductArt />` i
`Components/Shared/ProductCard.razor` med et almindeligt `<img>`.

`Components/Shared/TapAnimation.razor` er den animerede scene, hvor telefonen føres
hen til standeren, og skærmen skifter til anmeldelsessiden. Den kører på ren CSS
uden JavaScript, og selve forløbet styres af `.ta-*`-reglerne nederst i `app.css`.
Vil du gøre den hurtigere eller langsommere, retter du de fire steder, hvor der
står `7s`. Brugere, der har slået "reducér bevægelse" til i styresystemet, får et
stillbillede i stedet.

Illustrationerne bruger Googles fire farver, men ikke Googles logo. Skal jeres
rigtige produkter vises med logo, så brug fotos.

## Kontaktformularen

Hver henvendelse fra `/kontakt` bliver:

1. gemt som en JSON-fil (lokalt i `src/RateNow.Web/App_Data/henvendelser/`, på Azure i
   `/home/data/henvendelser`), så intet går tabt, og
2. sendt på mail til adressen i `appsettings.json` → `Smtp:To`. Trykker du *svar* på
   mailen, går svaret direkte til kunden.

Mailen sendes via Gmail (`smtp.gmail.com`). Adgangskoden står **ikke** i koden — den er en
Gmail-app-adgangskode, der ligger på serveren som indstillingen `Smtp__Password`. Mangler den,
gemmes henvendelserne kun som fil. Kan mailen ikke sendes, får kunden besked om at skrive
direkte til jeres e-mail.

Koden ligger i `Services/EmailContactSubmissionSink.cs`.

Formularen har en skjult honeypot-felt mod robotter og kræver et samtykke-flueben,
inden den kan sendes.

## Udgivelse

Siden udgives automatisk til <https://ratenow-hjemmeside.azurewebsites.net>, hver gang
der pushes til `main` (se `.github/workflows/deploy.yml`). Den kører på Azure App Service
(ressourcegruppe `ratenow-rg`, Sweden Central). Henvendelser fra kontaktformularen gemmes
dér i `/home/data/henvendelser`.

Den gratis plan (F1) lægger siden i dvale, når den ikke bliver brugt. Derfor besøger
`.github/workflows/keep-awake.yml` siden hvert 10. minut. GitHub slår planlagte
workflows fra, hvis der ikke er sket noget i repoet i 60 dage — så tjek fanen
**Actions** en gang imellem.

Vil du bygge en udgave selv:

```bash
dotnet publish src/RateNow.Web -c Release -o publish
```

Mappen `publish` kan lægges på enhver host, der kan køre ASP.NET Core 9 —
for eksempel Azure App Service, en Linux-server med Nginx foran, eller en container.

Husk på en produktionsserver:

- Sæt `ASPNETCORE_ENVIRONMENT=Production`
- Sørg for HTTPS (siden sender selv brugeren videre fra http)
- `App_Data` skal være skrivbar, hvis du stadig bruger den filbaserede modtager
