function defultselecteduser(object) {
    for (let x in object) {
        object[x].unselectedbtn.removeClass("button-36").removeClass("disabled").addClass("button-35");
    }
}
function selectedtypeofusers(btnselected, id) {
    let unselected = []
    switch (id) {
        case 1:
            btnselected.removeClass("button-35").addClass("button-36").addClass("disabled");
            loadusers(0);
            unselected = [
                { unselectedbtn: $("#activeuser"), id: 2 },
                { unselectedbtn: $("#deactiveuser"), id: 3 }
            ]
            defultselecteduser(unselected)
            break;
        case 2:
            btnselected.removeClass("button-35").addClass("button-36").addClass("disabled");
            loadusers(1);
            unselected = [
                { unselectedbtn: $("#alluser"), id: 1 },
                { unselectedbtn: $("#deactiveuser"), id: 3 }
            ]
            defultselecteduser(unselected)
            break;
        case 3:
            btnselected.removeClass("button-35").addClass("button-36").addClass("disabled");
            loadusers(2)
            unselected = [
                { unselectedbtn: $("#alluser"), id: 1 },
                { unselectedbtn: $("#activeuser"), id: 2 }
            ]
            defultselecteduser(unselected)
            break;
    }
}
function loadusers(x) {
    switch (x) {
        case 0:
            fetch('/Home/UsersTypeTable', {
                method: 'POST',
                cache: 'no-cache',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: x })
            })
                .then(response => response.text())
                .then(data => {
                    $("#divusertable").html(data);
                })
                .catch(error => {
                    alert("Error")
                })
            break;
        case 1:
            fetch('/Home/UsersTypeTable', {
                method: 'POST',
                cache: 'no-cache',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: x })
            })
                .then(response => response.text())
                .then(data => {
                    $("#divusertable").html(data);
                })
                .catch(error => {
                    alert("Error")
                })
            break;
        case 2:
            fetch('/Home/UsersTypeTable', {
                method: 'POST',
                cache: 'no-cache',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: x })
            })
                .then(response => response.text())
                .then(data => {
                    $("#divusertable").html(data);
                })
                .catch(error => {
                    alert("Error")
                })
            break;
    }
}
function Adduserloadpage() {
    fetch('/Home/Adduserpage', {
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
function cancelnewuser() {
    loadUserpage();
}

function savenewuser() {
    let edit =  document.getElementById('useredit').value;
    let picurl = $("#adduserpic").attr('src');
    let name = document.getElementById('saveusername').value;
    let lastname = document.getElementById('saveuserlastname').value;
    let fathername = document.getElementById('saveuserfathername').value;
    let melicode = Number(document.getElementById('saveusermelicode').value);
    let phonenumber = document.getElementById('saveuserphonenumber').value;
    let education = Number($("#saveusereducation").find(":selected").val());
    let email = document.getElementById('saveuseremail').value;
    let sex = Number($("#saveusergendre").find(":selected").val());

    const users = [
        { FirstName: name, LastName: lastname, FatherName: fathername, MdliCode: melicode, Email: email, Gender: sex, EducationDegree: education, PicURL: picurl, PhoneNumber: phonenumber }
    ]

    console.log(users)
    fetch('/Home/Saveuser', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({edit, users })
    })
        .then(response => response.text())
        .then(data => {
            alert("اطلاعات با موفقیت ذخیره شد");
            loadpage(1);
        })
        .catch(error => {
            alert("Error")
        })
}

function userdelete(Id) {
    if (confirm("آیا از حذف این کاربر مطمئن هستید؟")) {
        let id = Number(Id);
        fetch('/Home/deleteuser', {
            method: 'POST',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id })
        })
            .then(response => response.text())
            .then(data => {
                alert("اطلاعات با موفقیت ذخیره شد");
                loadusers(0);
            })
            .catch(error => {
                alert("Error")
            })
    }
}

function useredit(id) {
    fetch('/Home/Adduserpage', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id })
    })
        .then(response => response.text())
        .then(data => {
            $("#loadpartial").html(data);
        })
        .catch(error => {
            alert("Error")
        })
}