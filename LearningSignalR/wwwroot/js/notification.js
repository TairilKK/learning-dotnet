var connectionNotification = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notification").build();

document.querySelector("#sendButton").disabled = true;

connectionNotification.on("LoadNotifications", (message, counter) => {
    document.querySelector("#notificationCounter").innerHTML = "<span>(" + counter + ")</span>";
    document.querySelector("#messageList").innerHTML = "";

    for (let i = message.length - 1; i >= 0; i--) {
        var li = document.createElement("li");
        li.textContent = "Notification - " + message[i];
        document.querySelector("#messageList").appendChild(li);
    }
})

document.querySelector("#sendButton").addEventListener("click", (event) => {
    var message = document.querySelector("#notificationInput").value;
    connectionNotification.send("SendNotification", message).then(() => {
        document.querySelector("#notificationInput").value = "";
    })
    event.preventDefault();
});

connectionNotification.start().then(() => {
    connectionNotification.send("LoadMessages");
    document.querySelector("#sendButton").disabled = false;
})


