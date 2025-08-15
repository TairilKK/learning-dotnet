let lbl_houseJoined = document.getElementById("lbl_houseJoined");

let btn_un_gryffindor = document.getElementById("btn_un_gryffindor");
let btn_un_slytherin = document.getElementById("btn_un_slytherin");
let btn_un_hufflepuff = document.getElementById("btn_un_hufflepuff");
let btn_un_ravenclaw = document.getElementById("btn_un_ravenclaw");

let btn_gryffindor = document.getElementById("btn_gryffindor");
let btn_slytherin = document.getElementById("btn_slytherin");
let btn_hufflepuff = document.getElementById("btn_hufflepuff");
let btn_ravenclaw = document.getElementById("btn_ravenclaw");

let trigger_gryffindor = document.getElementById("trigger_gryffindor");
let trigger_slytherin = document.getElementById("trigger_slytherin");
let trigger_hufflepuff = document.getElementById("trigger_hufflepuff");
let trigger_ravenclaw = document.getElementById("trigger_ravenclaw");

var connectionHouseGroup = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/houseGroup", signalR.HttpTransportType.WebSockets).build();

btn_gryffindor.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("JoinGroup", "Gryffindor");
})

btn_hufflepuff.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("JoinGroup", "Hufflepuff");
})

btn_ravenclaw.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("JoinGroup", "Ravenclaw");
})

btn_slytherin.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("JoinGroup", "Slytherin");
})

btn_un_gryffindor.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("LeaveGroup", "Gryffindor");
})

btn_un_hufflepuff.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("LeaveGroup", "Hufflepuff");
})

btn_un_ravenclaw.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("LeaveGroup", "Ravenclaw");
})

btn_un_slytherin.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("LeaveGroup", "Slytherin");
})

trigger_gryffindor.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("TriggerGroupNotify", "Gryffindor");
})

trigger_hufflepuff.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("TriggerGroupNotify", "Hufflepuff");
})

trigger_ravenclaw.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("TriggerGroupNotify", "Ravenclaw");
})

trigger_slytherin.addEventListener("click", (event) => {
    event.preventDefault();
    connectionHouseGroup.send("TriggerGroupNotify", "Slytherin");
})

connectionHouseGroup.on("tiggerHouseNotification", houseName => {
    toastr.success(`A new notification for ${houseName} has been launched.`);
})

connectionHouseGroup.on("memberAddedToHouse", houseName => {
    toastr.success("New member has joined the " + houseName + " house!");
})

connectionHouseGroup.on("memberRemovedFromHouse", houseName => {
    toastr.success("Member has left the " + houseName + " house!");
})

connectionHouseGroup.on("subscitpionStatus", (strGroupsJoined, houseName, hasSubscribed) => {
    lbl_houseJoined.innerText = strGroupsJoined;
    if (hasSubscribed) {
        switch (houseName) {
            case "slytherin":
                btn_slytherin.style.display = "none";
                btn_un_slytherin.style.display = "";
                break;
            case "gryffindor":
                btn_gryffindor.style.display = "none";
                btn_un_gryffindor.style.display = "";
                break;
            case "hufflepuff":
                btn_hufflepuff.style.display = "none";
                btn_un_hufflepuff.style.display = "";
                break;
            case "ravenclaw":
                btn_ravenclaw.style.display = "none";
                btn_un_ravenclaw.style.display = "";
                break;
            default:
                break;
        }
        toastr.success("You have joined the " + houseName + " house!");
    } else {
        switch (houseName) {
            case "slytherin":
                btn_slytherin.style.display = "";
                btn_un_slytherin.style.display = "none";
                break;
            case "gryffindor":
                btn_gryffindor.style.display = "";
                btn_un_gryffindor.style.display = "none";
                break;
            case "hufflepuff":
                btn_hufflepuff.style.display = "";
                btn_un_hufflepuff.style.display = "none";
                break;
            case "ravenclaw":
                btn_ravenclaw.style.display = "";
                btn_un_ravenclaw.style.display = "none";
                break;
            default:
                break;
        }
        toastr.success("You have left the " + houseName + " house!");

    }
})


function houseGroupFulfilled() {
    console.log("Connection to House Group Hub successful!");
}
function rejected() {
    console.log("Connection to House Group Hub unsuccessful :(");
}

connectionHouseGroup.start().then(houseGroupFulfilled, rejected);

