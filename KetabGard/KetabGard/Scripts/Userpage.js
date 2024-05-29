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

const activeuserfordays = (id,self) => {
    let date = getdatenow()
    date = date.split(" ")

    let day = Number(date[0]);
    let month;
    let year = Number(date[2]);
    switch (date[1]) {
        case "فروردین":
            month = 1;
            break;
        case "اردیبهشت":
            month = 2;
            break;
        case "خرداد":
            month = 3;
            break;
        case "تیر":
            month = 4;
            break;
        case "مرداد":
            month = 5;
            break;
        case "شهریور":
            month = 6;
            break;
        case "مهر":
            month = 7;
            break;
        case "آبان":
            month = 8;
            break;
        case "آذر":
            month = 9;
            break;
        case "دی":
            month = 10;
            break;
        case "بهمن":
            month = 11;
            break;
        case "اسفند":
            month = 12;
            break;
    }
    fetch('/Home/calculateexpiredate', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: id, day: day, month: month, year: year })
    })
        .then(response => response.text())
        .then(data => {
            let parsedata = JSON.parse(data);
            self.style.backgroundColor = "rgba(48,77,48,0.25)";
            self.innerText = parsedata.result;
        })
        .catch(error => {
            alert("Error")
        })
}