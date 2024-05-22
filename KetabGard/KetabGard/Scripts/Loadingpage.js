function starttimeout() {
    return new Promise((resolve, reject) => {
        fetch('/Home/loader', {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json'
            }
        })
            .then(response => response.text())
            .then(data => {
                $("#loadpartial").html(data);
                var timeousttime = Math.floor(Math.random() * (2500 - 1000)) + 1000;
                timeout = setTimeout(function () {
                    resolve();
                }, timeousttime);
            })
            .catch(error => {
                alert("Error");
            });
    });
}