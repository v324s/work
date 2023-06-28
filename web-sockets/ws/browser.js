if (!window.WebSocket) {
	document.body.innerHTML = 'WebSocket в этом браузере не поддерживается.';
}

// создать подключение
var socket = new WebSocket("ws://localhost:8081");

// отправить сообщение из формы publish
document.forms.publish.onsubmit = function() {
  var outgoingMessage = this.message.value;

  socket.send(JSON.stringify({"author":this.name.value,"text":outgoingMessage}));
  return false;
};

// обработчик входящих сообщений
socket.onmessage = function(event) {
  var incomingMessage = event.data;
  showMessage(incomingMessage); 
};
//newBuffer = bufferFromBufferString('<Buffer 54 68 69 73 20 69 73 20 61 20 62 75 66 66 65 72 20 65 78 61 6d 70 6c 65 2e>')
//newBuffer.toString()
// показать сообщение в div#subscribe
var mess
function showMessage(message) {
  var messageElem = document.createElement('div');
  mess = JSON.parse(message);
  console.log(message);
  //newBuffer = bufferFromBufferString(mess['message']['data']);
  messageElem.appendChild(document.createTextNode(mess["author"]+": "+mess["message"]));
  document.getElementById('subscribe').appendChild(messageElem);
}
