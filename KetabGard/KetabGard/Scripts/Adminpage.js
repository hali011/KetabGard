function Addadminpageload() {
    fetch('/Home/Addadminpage', {
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
        })
        .catch(error => {
            alert("Error")
        })
}
function cancelnewadmin() {
    loadAdminpage()
}
function uploadpicture() {
    $('#imgupload').trigger('click');
}