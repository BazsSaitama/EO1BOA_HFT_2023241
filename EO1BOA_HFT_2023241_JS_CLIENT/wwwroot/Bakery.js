function Back() {
    window.location.href = "index.html";
}
let bakeries = [];
let connection = null;
let bakeryIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:39340/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BakeryCreated", (user, message) => {
        getdata();
    });

    connection.on("BakeryDeleted", (user, message) => {
        getdata();
    });

    connection.on("BakeryUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}


async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {

    await fetch("http://localhost:39340/Bakery")
        .then(x => x.json())
        .then(y => {
            bakeries = y;
            console.log(bakeries);
            display();
        });
}

function display() {
    document.getElementById('BakeryResults').innerHTML = "";

    bakeries.forEach(t => {
        document.getElementById('BakeryResults').innerHTML +=
            "<tr><td>" + t.bakeryId + "</td><td>" + t.name + "</td><td>" + t.location + "</td><td>" + t.rating + "</td><td>"
            + `<button type="button" onclick="remove(${t.bakeryId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.bakeryId})">Update</button>`
            + " </td></tr>";
    });
}

function Create() {
    let bakeryname = document.getElementById('BakeryName').value;
    let bakerylocation = document.getElementById('BakeryLocation').value;
    let bakeryrating = document.getElementById('BakeryRating').value;

    let asd = JSON.stringify({ name:bakeryname,location:bakerylocation,rating:bakeryrating });

    fetch('http://localhost:39340/Bakery', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: asd,
    }).then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    console.log(id);
    fetch('http://localhost:39340/Bakery/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();

        })
        .catch((error) => { console.error('Error:', error); });

}

function showupdate(id) {
    document.getElementById('BakeryUpdateName').value = bakeries.find(t => t['bakeryId'] == id)['name'];
    document.getElementById('BakeryUpdateLocation').value = bakeries.find(t => t['bakeryId'] == id)['location'];
    document.getElementById('BakeryUpdateRating').value = bakeries.find(t => t['bakeryId'] == id)['rating'];
    document.getElementById('UpdateForm').style.display = 'flex';
    bakeryIdToUpdate = id;
}

function Update() {
    document.getElementById('UpdateForm').style.display = 'none';
    let bakeryname = document.getElementById('BakeryUpdateName').value;
    let bakerylocation = document.getElementById('BakeryUpdateLocation').value;
    let bakeryrating = document.getElementById('BakeryUpdateRating').value;
    fetch('http://localhost:39340/Bakery', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {bakeryId:bakeryIdToUpdate,name:bakeryname,location:bakerylocation,rating:bakeryrating })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}
