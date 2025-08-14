// create connection
var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build();

// connect to methods that hub invokes
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.querySelector("#totalViewsCounter");
    newCountSpan.innerText = value.toString();
})

// invoke hub methods
function newWindowLoadedOnClient() {
    connectionUserCount.send("NewWindowLoaded");
}

// start connection
function fulfilled() {
    // do something on start
    console.log("Connection to User Hub successful!");
}

function rejected() {
    // rejected logs
    console.log("Connection to User Hub unsuccessful :(");
}

connectionUserCount.start().then(fulfilled, rejected);
