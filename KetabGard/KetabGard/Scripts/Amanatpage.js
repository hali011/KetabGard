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