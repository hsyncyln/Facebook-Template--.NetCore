'use strict';

var SIGNALR_URI = '../../chathub';
console.log("616515");
let posttext = null;
let friendonline = null;
let connection = null;

Initialize();

function Initialize() {
    posttext = document.getElementById("post-text");
    friendonline = document.getElementById("friend-online");
    InitializeEvent();
}

function InitializeEvent() {
    posttext.addEventListener('keypress', checkMessage);
    friendonline.addEventListener('click', MessageBoxOpen);
}
function checkMessage(e) {
    if (e.key == 'Enter') {
        connection.invoke("SendMessage", posttext.value);
    }
}

function MessageBoxOpen(e) {
    
    let value = friendonline.value;
    console.log(value+"+++++");
    let tre = value.indexOf("-");
    let friendid = value.substring(0, tre);
    let userid = value.substring(tre + 1);

    let mdownbox = document.getElementById("message-down-box");
    let mbox = document.getElementById("message-box");

    mdownbox.style["display"] = "none";
    mbox.style["display"] = "block";
    
    connectServer(friendid, userid);
    
}

function CreateMessageBox(user) {

    let mboxphoto = document.getElementsById("prof-pic-left");

    mboxphoto.src = "~/imgs/" + user.ProfilePhoto.ImageLink;

    let mboxname = document.getElementsById("message-box-name");
    mboxname.innerText = user.FirstName + user.LastName;

}
    
function connectServer(friendid,userid) {
 
  connection = new signalR.HubConnectionBuilder()
    .withUrl(SIGNALR_URI)
    .build();

  connection.start()
    .then(function () {
      connection.invoke('GetUser',userid,friendid);
      connection.invoke('GetMessages', userid, friendid);
      InitializeSignalREvent();
  //    setConnected();

    })
    .catch(function (err) {
//      setDisconnected();
    });
}

function InitializeSignalREvent() {

  connection.on('CreateMessageBox', OnNewMessage);
  connection.on('ShowMessages', OnNewMessage);
  connection.on('NewMessage', OnNewMessage);
  connection.onclose(OnClose);
}

function addMessage(content) {
  var messageElement = document.createElement('li');
  messageElement.textContent = content;
  messageElement.className = 'text-center';
  messageList.insertAdjacentElement('beforeend', messageElement);
}

function ShowMessage(messagelist) {

    let divmessages = document.getElementById("messages");

 /*   for(Message m in messagelist){

        let img = document.createElement("img");
        img.src = "~/imgs/" + message.ProfilePhoto.ImageLink;
        img.classList.add("prof-pic post-prof-pic comment-prof-pic");

        divmessages.appendChild(img);

        let divshape = document.createElement("div");
        divshape.classList.add("comment-wrapper");
        divshape.style.cssText += "height:auto;display:block; ";

        divmessages.appendChild(divshape);

        let divcontent = document.createElement("div");
        divcontent.classList.add("comment-text");
        divcontent.style.cssText += "width:auto;height:auto;background-color:rgb(233,235,238);";
        divcontent.innerHTML = message.Content + " &nbsp;&nbsp;";

        let divbottom = document.createElement("div");
        divbottom.style.cssText += "width:auto;height:6px;";

        divcontent.appendChild(divbottom);

        divmessages.appendChild(divcontent);
      
    }
    */
}

/*
function OnJoin(date, username, count) {
  addMessage(username + ' joined.');
  chatBody.scrollTop = chatBody.scrollHeight;
}

function OnLeft(date, username, count) {
  addMessage(username + ' left.');
  chatBody.scrollTop = chatBody.scrollHeight;
}
*/
function OnNewMessage(date, username, message) {

    let divmessages = document.getElementById("messages");

    let divshape = document.createElement("div");
    divshape.classList.add("comment-wrapper");
    divshape.style.cssText += "height:auto;display:block; ";

    divmessages.appendChild(divshape);

    let divcontent = document.createElement("div");
    divcontent.classList.add("comment-text");
    divcontent.style.cssText += "width:auto;height:auto;background-color:rgb(233,235,238);";
    divcontent.innerHTML = message + " &nbsp;&nbsp;";

    let divbottom = document.createElement("div");
    divbottom.style.cssText += "width:auto;height:6px;";

    divcontent.appendChild(divbottom);

    divmessages.appendChild(divcontent);
}
/*
function OnClose(err) {
  setDisconnected();
  setStatus(err);
}

function setConnected() {
  onlineStatus.classList.remove('d-none');
  chatForm.classList.add('d-none');
  chatBody.classList.remove('d-none');
  chatFooter.classList.remove('d-none');
}

function setDisconnected() {
  onlineStatus.classList.add('d-none');
  chatForm.classList.remove('d-none');
  chatBody.classList.add('d-none');
  chatFooter.classList.add('d-none');
}*/

function checkValue(value) {
  return value == null || value.trim() == '';
}

function sendMessage(text) {
  var isEmpty = checkValue(text);

  if (isEmpty) {
      return;
  }

  connection.invoke('SendMessage', text);
  posttext.value = '';
}

document.addEventListener('DOMContentLoaded', Initialize);