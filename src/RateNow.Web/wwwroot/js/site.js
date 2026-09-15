// Små UI-detaljer der ikke kræver en Blazor-forbindelse.
(function () {
    "use strict";

    function closeMobileNav() {
        var toggle = document.getElementById("nav-toggle");
        if (toggle) { toggle.checked = false; }
    }

    function updateHeaderShadow() {
        var header = document.querySelector(".site-header");
        if (header) { header.classList.toggle("is-stuck", window.scrollY > 8); }
    }

    // Luk mobilmenuen når der klikkes på et link i den.
    document.addEventListener("click", function (e) {
        if (e.target instanceof Element && e.target.closest(".nav a")) {
            closeMobileNav();
        }
    });

    document.addEventListener("keydown", function (e) {
        if (e.key === "Escape") { closeMobileNav(); }
    });

    window.addEventListener("scroll", updateHeaderShadow, { passive: true });
    updateHeaderShadow();

    // Blazors "enhanced navigation" udskifter indholdet uden en rigtig sideindlæsning,
    // så vi rydder op selv, når en ny side er hentet.
    if (window.Blazor) {
        Blazor.addEventListener("enhancedload", function () {
            closeMobileNav();
            updateHeaderShadow();
        });
    }
})();
