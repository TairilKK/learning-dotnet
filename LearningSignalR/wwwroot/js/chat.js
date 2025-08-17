var connectionChat = new signalR.HubConnectionBuilder().withUrl("/hubs/chat").build();

document.querySelector("#sendMessage").disabled = true;

connectionChat.on("MessageReceived", (user, message) => {
    let li = document.createElement("li");
    document.querySelector("#messagesList").appendChild(li);
    li.textContent = `${user} - ${message}`;
});

document.querySelector("#sendMessage").addEventListener("click", (event) => {
    event.preventDefault();
    let sender = document.querySelector("#senderEmail").value;
    let message = document.querySelector("#chatMessage").value;
    let receiver = document.querySelector("#receiverEmail").value;

    if (receiver.length > 0) {
        connectionChat.send("SendMessageToReceiver", sender, receiver, message);
    }
    else {
        connectionChat.send("SendMessageToAll", sender, message);
    }
});
connectionChat.start().then(() => {
    document.querySelector("#sendMessage").disabled = false;
});
