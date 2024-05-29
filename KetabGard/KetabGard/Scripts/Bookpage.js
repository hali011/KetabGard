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
const newgenre = () => {
    if ($("#genreselect").find(":selected").val() == 0) {
        $("#genreselect").hide();
        $("#newgenreinput").show();
        $("#cancelnewgenre").show();
    }
}
const cancelnewgenrebtn = () => {
    $("#genreselect").show();
    $("#newgenreinput").hide();
    $("#cancelnewgenre").hide();
    $("#genreselect").val('defult').change();
}
function cancelnewbook() {
    loadBookpage()
}

const Savebook = () => {
    let id = $("#editbook").val();
    const pickurl = $("#addbookpic").attr('src');
    const bookname = $("#bookname").val();
    const bookwriter = $("#bookwriter").val();
    const newgenre = $("#newgenreinput").val();
    let Genre = $("#genreselect").find(":selected").val();
    const publication = $("#publication").val();
    const bookpages = $("#countofbook").val();
    if (Genre == "0") {
        fetch('/Home/Savegenrebook', {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ genre: newgenre })
        })
            .then(response => response.text())
            .then(data => {
                const obj = JSON.parse(data)
                Genre = obj.id;
                const book = {
                    Id: id, Name: bookname, Writer: bookwriter, Genre: Genre, YearofPublication: publication, Pages: bookpages, PicURL: pickurl
                }
                fetch('/Home/Savenewbook', {
                    method: 'POST',
                    cache: 'no-cache',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(book)
                })
                    .then(response => response.text())
                    .then(data => {
                        alert("با موفقیت ذخیره شد");
                        loadpage(2);
                    })
                    .catch(error => {
                        alert("Error")
                    })
            })
            .catch(error => {

            })
    } else {
        const book = {
            Id: id,Name: bookname, Writer: bookwriter, Genre: Genre, YearofPublication: publication, Pages: bookpages, PicURL: pickurl
        }
        fetch('/Home/Savenewbook', {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(book)
        })
            .then(response => response.text())
            .then(data => {
                alert("با موفقیت ذخیره شد");
                loadpage(2);
            })
            .catch(error => {
                alert("Error")
            })
    }
    
    
}

const delbook = (id) => {
    fetch('/Home/Delbook', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id:id })
    })
        .then(response => response.text())
        .then(data => {
            loadbooktable();
        })
        .catch(error => {
            alert("Error")
        })
}

const loadbooktable = () => {
    fetch('/Home/BookTable', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify()
    })
        .then(response => response.text())
        .then(data => {
            $("#booktable").html(data);
        })
        .catch(error => {
            alert("Error")
        })
}

const editbook = (id) => {
    fetch('/Home/Addbookpage', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: id })
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