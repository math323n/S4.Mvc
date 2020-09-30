const TABLE = document.querySelector("#productTable");
const DARK = document.querySelector("#dark");

function TableDarkMode() {
    TABLE.classList.add(".table-dark");
}

DARK.addEventListener("click", TableDarkMode(), false);

