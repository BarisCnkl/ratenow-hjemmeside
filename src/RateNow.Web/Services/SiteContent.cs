using RateNow.Web.Models;

namespace RateNow.Web.Services;

/// <summary>
/// Alt tekstindhold på siden samlet ét sted. Vil du rette en overskrift, en pris
/// eller tilføje et FAQ-spørgsmål, så gør du det her — ikke ude i Razor-filerne.
/// </summary>
public static class SiteContent
{
    // ---------------------------------------------------------------------
    // PRODUKTER  —  priser rettes her, ikke ude i Razor-filerne.
    // ---------------------------------------------------------------------
    public static readonly IReadOnlyList<Product> Products =
    [
        new Product(
            Slug: "stander",
            Name: "NFC-stander",
            Tagline: "Den synlige løsning til disken",
            Description: "En stabil bordstander, der står ved kassen, på bordet eller i receptionen. " +
                         "Gæsten holder sin telefon op til skiltet og lander direkte på jeres anmeldelsesside. " +
                         "Ingen app, ingen kode, ingen forklaring.",
            PriceLabel: "1.099 kr.",
            PriceNote: "pr. stk. (mængderabat gives ved køb af flere)",
            Highlights:
            [
                "Står godt fast og vælter ikke",
                "Tryk i jeres farver og med jeres logo",
                "NFC og QR-kode, så alle telefoner kan være med",
                "Klar til brug — vi koder den inden afsendelse"
            ],
            BestFor: "Butikker, restauranter, klinikker, køreskoler og receptioner",
            Icon: "stand",
            IsPopular: true),

        new Product(
            Slug: "kort",
            Name: "NFC-kort",
            Tagline: "Anmeldelsen med i lommen",
            Description: "Et kort i visitkortstørrelse, som medarbejderen har på sig. " +
                         "Perfekt når samtalen er slut, og kunden er glad — så er kortet fremme med det samme. " +
                         "Kan også ligge i posen eller på regningen.",
            PriceLabel: "949 kr.",
            PriceNote: "pr. stk. (mængderabat gives ved køb af flere)",
            Highlights:
            [
                "Passer i pungen, forklædet eller kittellommen",
                "Slidstærkt plastkort der tåler hverdagen",
                "Fås som stak til hele teamet",
                "Samme link som resten af jeres materiale"
            ],
            BestFor: "Håndværkere, frisører, kørelærere, sælgere og servicefolk på farten",
            Icon: "card"),

        new Product(
            Slug: "klistermaerke",
            Name: "NFC-klistermærke",
            Tagline: "Sidder der, hvor kunden alligevel kigger",
            Description: "Et rundt mærke der sættes på bordet, ruden, betalingsterminalen eller bagsiden af menukortet. " +
                         "Det fylder ingenting, men er der hver gang en kunde har et øjeblik til overs.",
            PriceLabel: "1.099 kr.",
            PriceNote: "pr. stk. (mængderabat gives ved køb af flere)",
            Highlights:
            [
                "Kraftig lim der holder til rengøring",
                "Fås i flere størrelser",
                "Billig måde at dække mange borde",
                "Kan skiftes ud uden at ændre jeres link"
            ],
            BestFor: "Caféer, barer, fitnesscentre og venteværelser",
            Icon: "sticker"),

        new Product(
            Slug: "skraeddersyet",
            Name: "Skræddersyet løsning",
            Tagline: "Når I har noget helt særligt i tankerne",
            Description: "Skal det matche jeres brandmanual? Skal I have 40 enheder ud til 12 afdelinger? " +
                         "Eller noget vi slet ikke har tænkt på endnu? Så tager vi en snak og finder ud af det sammen.",
            PriceLabel: "Pris efter aftale",
            PriceNote: "vi regner den ud sammen",
            Highlights:
            [
                "Eget design fra bunden",
                "Samlet levering til flere adresser",
                "Mængderabat ved større ordrer",
                "Vi siger til, hvis vi ikke er den rigtige løsning for jer"
            ],
            BestFor: "Kæder, franchises og virksomheder med flere lokationer",
            Icon: "custom")
    ];

    // ---------------------------------------------------------------------
    // SÅDAN VIRKER DET
    // ---------------------------------------------------------------------
    public static readonly IReadOnlyList<Step> Steps =
    [
        new Step(1, "Vi finder jeres link",
            "Du fortæller os, hvor anmeldelserne skal lande — Google, Trustpilot, Facebook eller et andet sted. " +
            "Har du ikke styr på linket, finder vi det for dig."),

        new Step(2, "Vi koder det og kommer med det",
            "Produktet bliver programmeret og klargjort. Så sender vi det med fragt — eller vi kører det ud " +
            "til jer selv og sætter det op på stedet. Du skal ikke installere noget eller oprette en konto."),

        new Step(3, "Kunden holder telefonen op",
            "Et lille bip, og anmeldelsessiden åbner af sig selv. Det virker på stort set alle nyere telefoner — " +
            "og QR-koden tager resten."),

        new Step(4, "I får flere anmeldelser",
            "De fleste kunder vil gerne anmelde. De glemmer det bare, når de kommer ud ad døren. " +
            "Her sker det, mens oplevelsen stadig er frisk.")
    ];

    // ---------------------------------------------------------------------
    // HVORFOR OS
    // ---------------------------------------------------------------------
    public static readonly IReadOnlyList<ValueProp> Values =
    [
        new ValueProp("no-subscription", "Ingen abonnement, ingen binding",
            "Du køber et produkt én gang. Der kommer ikke en månedlig regning, og vi låser ikke jeres link inde bag en konto."),

        new ValueProp("own", "I ejer jeres anmeldelser",
            "Anmeldelserne ligger på Google og Trustpilot — hos jer. Vi er bare vejen derhen. Holder I op med at bruge os, står de der stadig."),

        new ValueProp("help", "Vi sætter det op for dig",
            "Du behøver ikke være teknisk. Send os en mail, så klarer vi opsætningen, inden produktet forlader os."),

        new ValueProp("honest", "Vi siger det, som det er",
            "Hvis vi ikke tror, det giver mening for jeres forretning, siger vi det. Det er billigere for os begge end en skuffet kunde.")
    ];

    // ---------------------------------------------------------------------
    // FAQ
    // ---------------------------------------------------------------------
    public static readonly IReadOnlyList<FaqItem> Faq =
    [
        new FaqItem("Skal kunden downloade en app?",
            "Nej. NFC er indbygget i telefonen. Kunden holder telefonen op til produktet, og siden åbner af sig selv. " +
            "Der er også en QR-kode, hvis telefonen er ældre, eller NFC er slået fra.",
            "Teknik"),

        new FaqItem("Virker det på både iPhone og Android?",
            "Ja. iPhone fra og med iPhone 7 læser NFC fra låseskærmen, og det samme gør de fleste Android-telefoner " +
            "fra de seneste mange år. Skulle en telefon drille, er QR-koden der som backup.",
            "Teknik"),

        new FaqItem("Kan vi se, hvor mange der har brugt den?",
            "Ja, hvis I ønsker det. Vi kan sætte produktet op, så I kan se antallet af tryk og dermed om den bliver brugt. " +
            "Vi registrerer ikke, hvem kunden er.",
            "Teknik"),

        new FaqItem("Er der et abonnement?",
            "Nej. Du betaler for produktet, og så er det dit. Vi tjener penge, når du køber noget — ikke på en fast månedlig regning.",
            "Pris"),

        new FaqItem("Bliver det billigere, hvis vi køber flere?",
            "Priserne på siden gælder for ét stykke. Skal I bruge flere — til hele teamet, til alle bordene " +
            "eller til flere afdelinger — så tager vi en snak om prisen. Skriv hvor mange I regner med, " +
            "så vender vi tilbage med et samlet tilbud.",
            "Pris"),

        new FaqItem("Hvad hvis vi skifter anmeldelsesplatform?",
            "Så skriver du til os, og vi hjælper med at pege produktet et nyt sted hen. Du skal ikke købe nyt udstyr, " +
            "fordi I går fra Facebook til Google.",
            "Pris"),

        new FaqItem("Må man overhovedet bede kunder om anmeldelser?",
            "Ja — så længe du spørger alle og ikke kun de glade, og så længe du ikke betaler for anmeldelser. " +
            "Vores produkter gør det nemt at spørge alle på samme måde, og det er præcis sådan, Google og Trustpilot gerne vil have det.",
            "Regler"),

        new FaqItem("Kan I filtrere de dårlige anmeldelser fra?",
            "Nej, og det ville vi heller ikke gøre. Det er i strid med både Googles og Trustpilots regler og kan koste jer profilen. " +
            "Det, vi kan, er at gøre det lettere for de mange tilfredse kunder at nå frem.",
            "Regler"),

        new FaqItem("Kan vi mødes med jer, før vi beslutter os?",
            "Ja, gerne. Skriv til os, så aftaler vi et tidspunkt, hvor vi kommer ud og sætter os ned med jer. " +
            "Vi tager produkterne med, fortæller om dem og svarer på det, I er i tvivl om — også hvis I bare vil " +
            "høre mere og ikke er i nærheden af at købe noget. Et møde koster ingenting og forpligter jer ikke.",
            "Møde og levering"),

        new FaqItem("Hvordan får vi produktet ud til os?",
            "Det bestemmer I. Vi sender det med fragt, eller vi kommer forbi med det. Vælger I det sidste, " +
            "er det en fra RateNOW, der står med det i hånden, og så sætter vi det op på stedet og viser, " +
            "hvordan det bruges.",
            "Møde og levering"),

        new FaqItem("Hvor lang tid går der, før vi har det i hånden?",
            "Standardprodukter sender vi typisk inden for få hverdage. Skal der trykkes eget design, aftaler vi en dato, " +
            "når vi har set materialet.",
            "Møde og levering"),

        new FaqItem("Hvad hvis produktet holder op med at virke?",
            "Så skriver du til os. NFC-chippen har intet batteri og holder i mange år, men går der noget galt, finder vi en løsning.",
            "Møde og levering")
    ];

    public static IEnumerable<string> FaqCategories =>
        Faq.Select(f => f.Category).Distinct();

    public static Product? FindProduct(string slug) =>
        Products.FirstOrDefault(p => p.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
}

public sealed record ValueProp(string Icon, string Title, string Body);
