function defultmenu(object) {
    for (let x in object) {
        switch (object[x].id) {
            case 0:
                object[x].undivtag.removeClass("bgmenucustom");
                object[x].untxttag.removeClass("text-white").addClass("text-secondary");
                object[x].unimgtag.attr("src", "/Source/Dashboard.png");
                break;
            case 1:
                object[x].undivtag.removeClass("bgmenucustom");
                object[x].untxttag.removeClass("text-white").addClass("text-secondary");
                object[x].unimgtag.attr("src", "/Source/User.png");
                break;
            case 2:
                object[x].undivtag.removeClass("bgmenucustom");
                object[x].untxttag.removeClass("text-white").addClass("text-secondary");
                object[x].unimgtag.attr("src", "/Source/Book.png");
                break;
            case 3:
                object[x].undivtag.removeClass("bgmenucustom");
                object[x].untxttag.removeClass("text-white").addClass("text-secondary");
                object[x].unimgtag.attr("src", "/Source/Amanat.png");
                break;
        }
    }
}
function selectedmenu(divtag, imgtag, txttag, x) {
    let notselected = [];
    switch (x) {
        case 0:
            divtag.addClass("bgmenucustom");
            txttag.removeClass("text-secondary").addClass("text-white");
            imgtag.attr("src", "/Source/SelectedDashboard.png");
            notselected = [
                { undivtag: $('#User'), unimgtag: $('#imgUser'), untxttag: $('#txtUser'), id: 1 },
                { undivtag: $('#Books'), unimgtag: $('#imgBook'), untxttag: $('#txtBook'), id: 2 },
                { undivtag: $('#Amanat'), unimgtag: $('#imgAmanat'), untxttag: $('#txtAmanat'), id: 3 }
            ]
            defultmenu(notselected)
            SelectedAddadmin($('#divAddAdmin'), false)
            loadpage(x)
            break;
        case 1:
            divtag.addClass("bgmenucustom");
            txttag.removeClass("text-secondary").addClass("text-white");
            imgtag.attr("src", "/Source/SelectedUser.png");
            notselected = [
                { undivtag: $('#Dashboard'), unimgtag: $('#imgdashboard'), untxttag: $('#txtdashboard'), id: 0 },
                { undivtag: $('#Books'), unimgtag: $('#imgBook'), untxttag: $('#txtBook'), id: 2 },
                { undivtag: $('#Amanat'), unimgtag: $('#imgAmanat'), untxttag: $('#txtAmanat'), id: 3 }
            ]
            defultmenu(notselected)
            SelectedAddadmin($('#divAddAdmin'), false)
            loadpage(x)
            break;
        case 2:
            divtag.addClass("bgmenucustom");
            txttag.removeClass("text-secondary").addClass("text-white");
            imgtag.attr("src", "/Source/SelectedBook.png");
            notselected = [
                { undivtag: $('#Dashboard'), unimgtag: $('#imgdashboard'), untxttag: $('#txtdashboard'), id: 0 },
                { undivtag: $('#User'), unimgtag: $('#imgUser'), untxttag: $('#txtUser'), id: 1 },
                { undivtag: $('#Amanat'), unimgtag: $('#imgAmanat'), untxttag: $('#txtAmanat'), id: 3 }
            ]
            defultmenu(notselected)
            SelectedAddadmin($('#divAddAdmin'), false)
            loadpage(x)
            break;
        case 3:
            divtag.addClass("bgmenucustom");
            txttag.removeClass("text-secondary");
            txttag.addClass("text-white");
            imgtag.attr("src", "/Source/SelectedAmanati.png");
            notselected = [
                { undivtag: $('#Dashboard'), unimgtag: $('#imgdashboard'), untxttag: $('#txtdashboard'), id: 0 },
                { undivtag: $('#User'), unimgtag: $('#imgUser'), untxttag: $('#txtUser'), id: 1 },
                { undivtag: $('#Books'), unimgtag: $('#imgBook'), untxttag: $('#txtBook'), id: 2 }
            ]
            defultmenu(notselected)
            SelectedAddadmin($('#divAddAdmin'), false)
            loadpage(x)
            break;
    }
}

function SelectedAddadmin(divaddAdmin, selected) {
    if (selected) {
        let notselected = [
            { undivtag: $('#Dashboard'), unimgtag: $('#imgdashboard'), untxttag: $('#txtdashboard'), id: 0 },
            { undivtag: $('#User'), unimgtag: $('#imgUser'), untxttag: $('#txtUser'), id: 1 },
            { undivtag: $('#Books'), unimgtag: $('#imgBook'), untxttag: $('#txtBook'), id: 2 },
            { undivtag: $('#Amanat'), unimgtag: $('#imgAmanat'), untxttag: $('#txtAmanat'), id: 3 }
        ]
        defultmenu(notselected)
        divaddAdmin.css("background-color", "rgba(30,30,30,0.2)")
        let x = 4
        loadpage(x)
    } else {
        divaddAdmin.css("background-color", "")
    }
}

function loadpage(number) {
    if (timeout) {
        clearTimeout(timeout);
    }
    starttimeout()
        .then(() => {
            switch (number) {
                case 0:
                    loadDashboard()
                    break;
                case 1:
                    loadUserpage()
                    break;
                case 2:
                    loadBookpage()
                    break;
                case 3:
                    loadAmanatpage()
                    break;
                case 4:
                    loadAdminpage()
                    break;
            }
        })
}

function getdatenow() {
    const d = new Date();
    let gy = d.getFullYear();
    let gm = d.getMonth() + 1;
    let gd = d.getDate();
    let jalalidate = gregorian_to_jalali(gy, gm, gd);
    let datenow;
    let datemonth;
    switch (jalalidate[1]) {
        case 1:
            datemonth = "فروردین"
            break;
        case 2:
            datemonth = "اردیبهشت"
            break;
        case 3:
            datemonth = "خرداد"
            break;
        case 4:
            datemonth = "تیر"
            break;
        case 5:
            datemonth = "مرداد"
            break;
        case 6:
            datemonth = "شهریور"
            break;
        case 7:
            datemonth = "مهر"
            break;
        case 8:
            datemonth = "آبان"
            break;
        case 9:
            datemonth = "آذر"
            break;
        case 10:
            datemonth = "دی"
            break;
        case 11:
            datemonth = "بهمن"
            break;
        case 12:
            datemonth = "اسفند"
            break;
    }
    datenow = jalalidate[2] + " " + datemonth + " " + jalalidate[0];
    return datenow
}

function loadDashboard() {
    fetch('/Home/Dashboard', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: 1 })
    })
        .then(response => response.text())
        .then(data => {
            clearTimeout(timeout);
            $("#loadpartial").html(data);
            loaddashboardtable();
            const datenow = getdatenow();
            $("#nowdate").append(datenow);
            Cookies.set('state', 'Dashboard');
        })
        .catch(error => {
            alert("Error")
        })
}
function loadUserpage() {
    fetch('/Home/Userpage', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: 1 })
    })
        .then(response => response.text())
        .then(data => {
            clearTimeout(timeout);
            $("#loadpartial").html(data);
            loadusers(0);
            const datenow = getdatenow()
            $("#nowdate").append(datenow);
            Cookies.set('state', 'Userpage');
        })
        .catch(error => {
            alert("Error")
        })
}
function loadBookpage() {
    fetch('/Home/Bookpage', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: 1 })
    })
        .then(response => response.text())
        .then(data => {
            clearTimeout(timeout);
            $("#loadpartial").html(data);
            loadbooktable()
            const datenow = getdatenow()
            $("#nowdate").append(datenow);
            Cookies.set('state', 'Bookpage');
        })
        .catch(error => {
            alert("Error")
        })
}
function loadAmanatpage() {
    fetch('/Home/Amanatpage', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: 1 })
    })
        .then(response => response.text())
        .then(data => {
            clearTimeout(timeout);
            $("#loadpartial").html(data);
            loadamanatis(0);
            const datenow = getdatenow();
            $("#nowdate").append(datenow);
            Cookies.set('state', 'Amanatpage');
        })
        .catch(error => {
            alert("Error")
        })
}
function loadAdminpage() {
    fetch('/Home/Adminpage', {
        method: 'POST',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ id: 1 })
    })
        .then(response => response.text())
        .then(data => {
            clearTimeout(timeout);
            $("#loadpartial").html(data);
            admintableload();
            Cookies.set('state', 'Adminpage');
        })
        .catch(error => {
            alert("Error")
        })
}