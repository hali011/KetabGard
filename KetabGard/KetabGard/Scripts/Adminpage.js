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
            $("#newaccessname").hide()
            $("#cancelnewaccess").hide()
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

const admintableload = () => {
    fetch('/Home/AdminTable', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: 1 })
    })
        .then(response => response.text())
        .then(data => {
            $("#admintable").html(data);
        })
        .catch(error => {
            alert("Error")
        })
}

const deladmin = (id) => {
    debugger;
    fetch('/Home/DelAdmin', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: id })
    })
        .then(response => response.text())
        .then(data => {
            admintableload();
        })
        .catch(error => {
            alert("Error")
        })
}

const changedropdown = () => {
    if ($("#accessdropdown").find(":selected").val() == 0) {
        $("#newaccessname").show()
        $("#accessdropdown").hide()
        $("#cancelnewaccess").show()
    }
}

const cancelaccess = () => {
    $("#newaccessname").hide()
    $("#cancelnewaccess").hide()
    $("#accessdropdown").show()
    $("#accessdropdown").val('defult').change();
}

const savenewadmin = () => {
    const id = $("#editadminid").val();
    const picurl = $("#addadminpic").attr("src");
    const name = $("#adminname").val();
    const lastname = $("#adminlastname").val();
    const codemeli = $("#adminmelicode").val();
    const username = $("#adminusername").val();
    const madrak = $("#saveadmineducation").find(":selected").val();
    const phonenumber = $("#adminphonenumber").val();
    const email = $("#adminemail").val();
    const password = $("#adminpassword").val();
    const newaccessname = $("#newaccessname").val();
    let accessid = $("#accessdropdown").find(":selected").val();

    if (accessid == 0) {
        fetch('/Home/AddnewAccess', {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ name : newaccessname })
        })
            .then(response => response.text())
            .then(data => {
                let dataparse = JSON.parse(data);
                accessid = dataparse.id;
                const admin = {
                    id: id, Name: name, Lastname: lastname, Mcode: codemeli, Username: username, Phonenumber: phonenumber, Email: email, Password: password, EducationDegree: madrak, Access: accessid, Picurl: picurl
                }
                fetch('/Home/Savenewadmin', {
                    method: 'POST',
                    cache: 'no-cache',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(admin)
                })
                    .then(response => response.text())
                    .then(data => {
                        alert("با موفقیت ذخیره شد");
                        loadpage(4);
                    })
                    .catch(error => {
                        alert("Error")
                    })
            })
            .catch(error => {
                alert("Error")
            })
    }
    else {
        const admin = {
            id: id, Name: name, Lastname: lastname, Mcode: codemeli, Username: username, Phonenumber: phonenumber, Email: email, Password: password, EducationDegree: madrak, Access: accessid, Picurl: picurl
        }
        fetch('/Home/Savenewadmin', {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(admin)
        })
            .then(response => response.text())
            .then(data => {
                alert("با موفقیت ذخیره شد");
                loadpage(4);
            })
            .catch(error => {
                alert("Error")
            })
    }
}

const editadmin = (id) => {
    fetch('/Home/Addadminpage', {
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
            $("#newaccessname").hide()
            $("#cancelnewaccess").hide()
        })
        .catch(error => {
            alert("Error")
        })
}