document.addEventListener("DOMContentLoaded", function () {
    const menu = document.getElementById("menu");
    const hamburgerMenu = document.getElementById("hamburgerMenu");
    const logo = document.getElementById("logo");

    hamburgerMenu.addEventListener("click", function () {
        menu.classList.toggle("active");
        logo.classList.toggle("active");
    });

    // Menü dışına tıklayınca kapatma
    document.addEventListener("click", function (event) {
        if (!menu.contains(event.target) && !hamburgerMenu.contains(event.target) && !logo.contains(event.target) ) {
            menu.classList.remove("active");
        }
    });
});