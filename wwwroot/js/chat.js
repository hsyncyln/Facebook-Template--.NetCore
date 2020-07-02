"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg =  msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    li.style.cssText += "width:auto;height:auto;background-color:rgb(233,235,238);float:right;padding:3%;padding-left:5%;padding-right:5%;margin-right:1%;";
    li.classList.add("comment-text");

    document.getElementById("messagesList").appendChild(li);

    var br = document.createElement("br");
    document.getElementById("messagesList").appendChild(br);

    var br2 = document.createElement("br");
    document.getElementById("messagesList").appendChild(br2);

    var br3 = document.createElement("br");
    document.getElementById("messagesList").appendChild(br3);


});

connection.on("WritePastMessage", function (deneme) {

    var user = document.getElementById("repo-user").value;
    var friend = document.getElementById("repo-friend").value;
    console.log(deneme);

    for (var item in deneme) {
        
        if (deneme[item][0] == user) {
            var li = document.createElement("li");
            li.textContent = deneme[item][1];
            li.style.cssText += "width:auto;height:auto;background-color:rgb(233,235,238);float:right;padding:3%;padding-left:5%;padding-right:5%;margin-right:1%;";
            li.classList.add("comment-text");

            document.getElementById("messagesList").appendChild(li);

            var br = document.createElement("br");
            document.getElementById("messagesList").appendChild(br);

            var br2 = document.createElement("br");
            document.getElementById("messagesList").appendChild(br2);

            var br3 = document.createElement("br");
            document.getElementById("messagesList").appendChild(br3);
        }
        else {
            var li = document.createElement("li");
            li.textContent = deneme[item][1];
            li.style.cssText += "width:auto;height:auto;background-color:rgb(233,235,238);float:left;padding:3%;padding-left:5%;padding-right:5%;margin-left:1%;";

            document.getElementById("messagesList").appendChild(li);
            li.classList.add("comment-text");

            var br = document.createElement("br");
            document.getElementById("messagesList").appendChild(br);

            var br2 = document.createElement("br");
            document.getElementById("messagesList").appendChild(br2);

            var br3 = document.createElement("br");
            document.getElementById("messagesList").appendChild(br3);

        }
    }
   
});



connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;  
    
}).catch(function (err) {
    return console.error(err.toString());
});

function GetPast() {
    console.log("burası");
    var user = document.getElementById("repo-user").value;
    console.log(user + "**");
    var friend = document.getElementById("repo-friend").value;
    console.log(user + " " + friend);
    connection.invoke("GetMessages", user, friend).catch(function (err) {
        return console.error(err.toString());
    });
}

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("repo-user").value;
    var friend = document.getElementById("repo-friend").value;
    var message = document.getElementById("messageInput").value;
    
    connection.invoke("SendMessage", user, friend, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});