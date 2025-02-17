document.addEventListener("DOMContentLoaded", function () {
    // current date in navbar
    var currentDateElement = document.getElementById("currentDate");
    if (currentDateElement) {
        var currentDate = new Date();
        var options = { year: 'numeric', month: 'numeric', day: 'numeric' };
        currentDateElement.textContent = "Today's date: " + currentDate.toLocaleDateString(undefined, options);
    }

    // current year in footer
    var footerYearElement = document.getElementById("currentYear");
    if (footerYearElement) {
        footerYearElement.textContent = new Date().getFullYear();
    }
});