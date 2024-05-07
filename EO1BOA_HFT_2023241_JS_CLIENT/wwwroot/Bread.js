function Back() {
    window.location.href = "index.html";
}
let breads = [];
let connection = null;
let breadIdToUpdate = -1;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:39340/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("BreadCreated", (user, message) => {
        getdata();
    });

    connection.on("BreadDeleted", (user, message) => {
        getdata();
    });

    connection.on("BreadUpdated", (user, message) => {
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

    await fetch("http://localhost:39340/Bread")
        .then(x => x.json())
        .then(y => {
            breads = y;
            console.log(breads);
            display();
        });
}

function display() {
    document.getElementById('BreadResults').innerHTML = "";
    
    breads.forEach(t => {
        document.getElementById('BreadResults').innerHTML +=
            "<tr><td>" + t.breadId + "</td><td>" + t.name + "</td><td>" + t.isDessert + "</td><td>" + t.weight + "</td><td>" + t.bakeryId + "</td><td>"
            + `<button type="button" onclick="remove(${t.breadId})">Delete</button>`
            +`<button>Update</button>`
            + " </td></tr>";
    });
}

function Create() {
    let breadname = document.getElementById('BreadName').value;
    let breadweight = document.getElementById('BreadWeight').value;
    let breadbakeryid = document.getElementById('BreadBakeryId').value;

    let asd = JSON.stringify({ bakeryId: breadbakeryid,name: breadname, weight: breadweight });

    fetch('http://localhost:39340/Bread', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: asd,
    })  .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
    .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    console.log(id);
    fetch('http://localhost:39340/Bread/' + id, {
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