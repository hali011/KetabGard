let amanatitablety;
function selectedtypeofAmanati(btnselected, id) {
    let unselected = []
    switch (id) {
        case 1:
            btnselected.removeClass("button-35").addClass("button-36").addClass("disabled");
            loadamanatis(0);
            unselected = [
                { unselectedbtn: $("#activeamanat"), id: 2 },
                { unselectedbtn: $("#deactiveamanat"), id: 3 }
            ]
            defultselecteduser(unselected)
            break;
        case 2:
            btnselected.removeClass("button-35").addClass("button-36").addClass("disabled");
            loadamanatis(1);
            unselected = [
                { unselectedbtn: $("#allamanati"), id: 1 },
                { unselectedbtn: $("#deactiveamanat"), id: 3 }
            ]
            defultselecteduser(unselected)
            break;
        case 3:
            btnselected.removeClass("button-35").addClass("button-36").addClass("disabled");
            loadamanatis(2)
            unselected = [
                { unselectedbtn: $("#allamanati"), id: 1 },
                { unselectedbtn: $("#activeamanat"), id: 2 }
            ]
            defultselecteduser(unselected)
            break;
    }
}
function loadamanatis(x) {
    amanatitablety = x;
    switch (x) {
        case 0:
            fetch('/Home/AmanatiTypeTable', {
                method: 'POST',
                cache: 'no-cache',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: x })
            })
                .then(response => response.text())
                .then(data => {
                    $("#divamanattable").html(data);
                })
                .catch(error => {
                    alert("Error")
                })
            break;
        case 1:
            fetch('/Home/AmanatiTypeTable', {
                method: 'POST',
                cache: 'no-cache',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: x })
            })
                .then(response => response.text())
                .then(data => {
                    $("#divamanattable").html(data);
                })
                .catch(error => {
                    alert("Error")
                })
            break;
        case 2:
            fetch('/Home/AmanatiTypeTable', {
                method: 'POST',
                cache: 'no-cache',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: x })
            })
                .then(response => response.text())
                .then(data => {
                    $("#divamanattable").html(data);
                })
                .catch(error => {
                    alert("Error")
                })
            break;
    }
}
function AddAmanatiloadpage() {
    fetch('/Home/Addamanatipage', {
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
function cancelnewamanati() {
    loadAmanatpage();
}

const selectedbook = (book) => {
    let id = document.getElementById("amanatbookid");
    let name = document.getElementById("amanatbookname");
    let writer = document.getElementById("amanatbookwriter");
    let publication = document.getElementById("amanatbookpublication");
    let pic = $("#amanatbookpic");
    
    id.value = book.Id;
    name.value = book.Name;
    writer.value = book.Writer;
    publication.value = book.YearofPublication; 
    pic.attr('src', book.PicURL);
}

const selecteduser = (user) => {
    let id = document.getElementById("amanatuserid");
    let name = document.getElementById("amanatusername");
    let lname = document.getElementById("amanatuserlname");
    let phonenumber = document.getElementById("amanatuserphonenumber");
    let pic = $("#amanatuserpic");
    
    id.value = user.Id;
    name.value = user.FirstName;
    lname.value = user.LastName;
    phonenumber.value = user.PhoneNumber;
    pic.attr('src', user.PicURL);
}

const savenewamanat = () => {
    setvalonmodal();
    let id = Number(document.getElementById("amanatid").value);
    let bookid = Number(document.getElementById("amanatbookid").value);
    let userid = Number(document.getElementById("amanatuserid").value);
    let username = document.getElementById("amanatusername").value;
    let bookname = document.getElementById("amanatbookname").value;
    let name = username + "-" + bookname;
    let expireday = 14;
    let active = true;
    let date = changemonth(getdatenow());
    let expiredate = getexpiredate(expireday, date);

    const amanat = {
        Id: id, Name: name, Date: date, Expiredate: expiredate, Userid: userid, Bookid: bookid, Expireday: expireday, Active: active
    }

    fetch('/Home/Savenewamanat', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(amanat)
    })
        .then(response => response.text())
        .then(data => {
            $('#successModal').modal('show');
            const settt = setTimeout(function () {
                $('#successModal').modal('hide');
                loadpage(3);
            }, 3000);
        })
        .catch(error => {
            alert("Error")
        })
}

const changemonth = (date) => {
    const d = date.split(" ");
    let years = d[2];
    let month;
    let days = d[0];

    switch (d[1]) {
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
    const result = years + " " + month + " " + days;
    return result;
}

const getexpiredate = (expd, d) => {
    let date = d.split(" ");
    let year = Number(date[0]);
    let month = Number(date[1]);
    let day = Number(date[2]);

    if (day + expd > 30) {
        if (month + 1 > 12) {
            month = (month + 1) - 12
            year++;
        } else {
            day = (day + expd) - 30;
            month++;
        }
    } else {
        day += expd; 
    }

    const result = year + " " + month + " " + day;
    return result;
}

const editamanat = (id) => {
    fetch('/Home/Addamanatipage', {
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
        })
        .catch(error => {
            alert("Error")
        })
}

const backed = (id) => {
    fetch('/Home/BackedAmanat', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: id })
    })
        .then(response => response.text())
        .then(data => {
            alert("عملیات با موفقیت انجام شد");
            loadamanatis(amanatitablety);
        })
        .catch(error => {
            alert("Error")
        })
}

const setvalonmodal = () => {
    const d = new Date();
    let gy = d.getFullYear();
    let gm = d.getMonth() + 1;
    let gd = d.getDate();
    let jalalidate = gregorian_to_jalali(gy, gm, gd);
    let year = jalalidate[0];
    let month = jalalidate[1];
    let day = jalalidate[2];

    if (day + 14 <= 30) {
        day += 14;
    } else {
        if (month + 1 <= 12) {
            month += 1;
        } else {
            year++;
            month = 1;
        }
    }

    switch (month) {
        case 1:
            month = "فروردین"
            break;
        case 2:
            month = "اردیبهشت"
            break;
        case 3:
            month = "خرداد"
            break;
        case 4:
            month = "تیر"
            break;
        case 5:
            month = "مرداد"
            break;
        case 6:
            month = "شهریور"
            break;
        case 7:
            month = "مهر"
            break;
        case 8:
            month = "آبان"
            break;
        case 9:
            month = "آذر"
            break;
        case 10:
            month = "دی"
            break;
        case 11:
            month = "بهمن"
            break;
        case 12:
            month = "اسفند"
            break;
    }

    const result = "تاریخ تحویل: " + year + "/" + month + "/" + day + "(14روز)";

    document.getElementById('tarikhtahvil').innerText = result;
}