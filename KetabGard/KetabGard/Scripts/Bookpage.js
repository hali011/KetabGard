function Addbookloadpage() {
    fetch('/Home/Addbookpage', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: 1 })
    })
        .then(response => response.text())
        .then(data => {
            $("#loadpartial").html(data);
            $("#newgenreinput").hide();
            $("#cancelnewgenre").hide();
        })
        .catch(error => {
            alert("Error")
        })
}
function newgenre() {
    $("#genreselect").hide();
    $("#newgenreinput").show();
    $("#cancelnewgenre").show();
}
function cancelnewgenrebtn() {
    $("#genreselect").show();
    $("#newgenreinput").hide();
    $("#cancelnewgenre").hide();
    $("#genreselect").val('0').change();
}
function cancelnewbook() {
    loadBookpage()
}