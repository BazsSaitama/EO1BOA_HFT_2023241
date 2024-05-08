function Back() {
    window.location.href = "index.html";
}
let ovens = [];
let connection = null;
let ovenIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:39340/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("OvenCreated", (user, message) => {
        getdata();
    });

    connection.on("OvenDeleted", (user, message) => {
        getdata();
    });

    connection.on("OvenUpdated", (user, message) => {
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

    await fetch("http://localhost:39340/Oven")
        .then(x => x.json())
        .then(y => {
            ovens = y;
            console.log(ovens);
            display();
        });
}

function display() {
    document.getElementById('OvenResults').innerHTML = "";

    ovens.forEach(t => {
        document.getElementById('OvenResults').innerHTML +=
            "<tr><td>" + t.breadId + "</td><td>" + t.bakingTime + "</td><td>" + t.breadCapacity + "</td><td>" + t.price + "</td><td>"
            + `<button type="button" onclick="remove(${t.ovenId})">Delete</button>`
            + `<button type="button" onclick="showupdate(${t.ovenId})">Update</button>`
            + " </td></tr>";
    });
}

function Create() {
    let ovenname = document.getElementById('OvenBakingTime').value;
    let ovencapacity = document.getElementById('OvenCapacity').value;
    let ovenprice = document.getElementById('OvenPrice').value;

    let asd = JSON.stringify({ bakingTime:ovenname,price:ovenprice,breadCapacity:ovencapacity });

    fetch('http://localhost:39340/Oven', {
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
    fetch('http://localhost:39340/Oven/' + id, {
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
    document.getElementById('OvenUpdateBaking').value = ovens.find(t => t['ovenId'] == id)['bakingTime'];
    document.getElementById('OvenUpdatePrice').value = ovens.find(t => t['ovenId'] == id)['price'];
    document.getElementById('OvenUpdateCapacity').value = ovens.find(t => t['ovenId'] == id)['breadCapacity'];
    document.getElementById('UpdateForm').style.display = 'flex';
    ovenIdToUpdate = id;
}

function Update() {
    document.getElementById('UpdateForm').style.display = 'none';
    let ovenbaking = document.getElementById('OvenUpdateBaking').value;
    let ovenprice = document.getElementById('OvenUpdatePrice').value;
    let ovencapacity = document.getElementById('OvenUpdateCapacity').value;
    fetch('http://localhost:39340/Oven', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { ovenId: ovenIdToUpdate,breadCapacity:ovencapacity,bakingTime:ovenbaking,price:ovenprice })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}