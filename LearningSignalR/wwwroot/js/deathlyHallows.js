var connectionDeathlyHallows = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/deathlyHallows", signalR.HttpTransportType.WebSockets).build();

var cloakSpan = document.querySelector("#cloakCounter");
var stoneSpan = document.querySelector("#stoneCounter");
var wandSpan = document.querySelector("#wandCounter");

connectionDeathlyHallows.on("updateDeathlyHallowCount", ( cloak, stone, wand ) => {
    cloakSpan.innerText = cloak.toString();
    stoneSpan.innerText = stone.toString();
    wandSpan.innerText = wand.toString();
})

function deathlyHallowsFulfilled() {
    connectionDeathlyHallows.invoke("GetRaceStatus")
        .then((raceCounter) => {
        cloakSpan.innerText = raceCounter.cloak.toString();
        stoneSpan.innerText = raceCounter.stone.toString();
        wandSpan.innerText = raceCounter.wand.toString();
    })
    console.log("Connection to Deathly Hub successful!");
}
function rejected() {
    console.log("Connection to Deathly Hub unsuccessful :(");
}

connectionDeathlyHallows.start().then(deathlyHallowsFulfilled, rejected);
