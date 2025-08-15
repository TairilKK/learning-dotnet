// create connection
var connectionUserCount = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Information)
    .withUrl("/hubs/userCount", signalR.HttpTransportType.WebSockets).build();

// connect to methods that hub invokes
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.querySelector("#totalViewsCounter");
    newCountSpan.innerText = value.toString();
})

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.querySelector("#totalUsersCounter");
    newCountSpan.innerText = value.toString();
})

// invoke hub methods
function NewWindowLoadedOnClient() {
    // if send no data seen
    connectionUserCount.invoke("NewWindowLoaded", "Name").then((data) => {
        console.log(data)
    });
}

// start connection
function fulfilled() {
    // do something on start
    console.log("Connection to User Hub successful!");
    NewWindowLoadedOnClient();
}

function rejected() {
    // rejected logs
    console.log("Connection to User Hub unsuccessful :(");
}

connectionUserCount.start().then(fulfilled, rejected);
