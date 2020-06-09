
var createRoomBtn = document.getElementById('create-room-btn');
var createRoomModel = document.getElementById('create-room-model');

createRoomBtn.addEventListener('click', function () {
    createRoomModel.classList.add('active');

});

function closeModel() {
    createRoomModel.classList.remove('active');
}

