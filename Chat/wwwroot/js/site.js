var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("aaaa").addEventListener("click", function () {
    alert("ssssssapapkdoapdjoadjaoj");
});
var _connectionId = '';

connection.on("ReciveMessage", function (data) {
    alert("ssssssssssssssssssssssssssssssssssss")


    var message = document.createElement("div");
    message.classList.add("message");
    alert("ssssssssssssssssssssssssssssssssssssxxxxxxxxxxxxxxx")
    //var header = document.createElement("header");
    // header.appendChild(document.createTextNode(data.name));
    alert(message.childElementCount);
    //var p = document.createElement("p");
    //p.appendChild(document.createTextNode(data.text));

    //var footer = document.createElement("footer");

    // footer.appendChild(document.createTextNode(date.timestap));
    // footer.textContent(date.timestap);
    //message.appendChild(header);
    //message.appendChild(p);
    //message.appendChild(footer);

    // document.querySelector('.chat-body').appendChild(message);
    alert("ssssssssssssssssssssssssssssssssssssxxxxxxxxxxxxx")

});



var joinRoom = function () {
    var url = '/chat/JoinRoom/' + _connectionId + '/@Model.Name';
    axios.post(url, null).then(res => {

        console.log('RoomJoin', res);
    }).catch(error => {

        console.error('room join error', error);
    });
};

connection.start().then(function () {
    connection.invoke("getConnectionId").then(function (connctionId) {

        _connectionId = connctionId;
        joinRoom();
    });

}).catch(function (error) {

    console.log(error);
});



var sendMessage = function (event) {

    event.preventDefault();

    var data = new FormData(event.target);
    document.getElementById('message-input').value = '';
    axios.post('/Chat/SendMessage', data).then(res => {

        console.log("Message Send", res);
    }).catch(err => {

        console.error("Feild Send", err);
    });
}






