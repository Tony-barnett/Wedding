function filterList() {
    var input, filter, ul, li, a, i;
    input = $("#filterBox")[0];
    filter = input.value.toUpperCase().trim();
    ul = $("#questions");

    $('#questions .q-and-a-section').each(function () {
        // if the title contains it then show the whole section...
        if (this.getElementsByTagName("h6")[0].innerText.toUpperCase().indexOf(filter) > -1 || filter === "") {
            this.style.display = "";
            $(this.getElementsByTagName("li")).each(function (_, li) {
                li.style.display = "";
            });
        } else {
            // .. otherwise only show an li if a question or answer contains the search text
            //var t = this.getElementsByTagName("div").innerText;
            $(this.getElementsByTagName("li")).each(function (_, li) {
                a = li.innerText.toUpperCase();
                if (a.indexOf(filter) > -1) {
                    li.style.display = "";
                } else {
                    li.style.display = "none";
                }
            });
        }
    });
}

$(document).ready(function () {
    $('form').on('submit', function (event) {
        event.preventDefault();
    });
});
