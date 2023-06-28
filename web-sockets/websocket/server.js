var http = require('http');
var Static = require('node-static');
var WebSocketServer = new require('ws');

// подключенные клиенты
var clients = {};

// WebSocket-сервер на порту 8081
var webSocketServer = new WebSocketServer.Server({port: 8081});
webSocketServer.on('connection', function(ws) {

  var id = Math.random();
  clients[id] = ws;
  console.log("новое соединение " + id);
  var arrmess;
  ws.on('message', function(message) {
    arrmess=JSON.parse(message);
    console.log(arrmess['author']+": "+arrmess['text']);
    if (message=='/getUsers'){
      console.log("Пользователи:");
      for(var key in clients) {
        console.log(clients[key]);
      }
    }else{
      console.log('получено сообщение ' + message);

      for(var key in clients) {
        //clients[key].send(JSON.stringify({'message': message}));
        clients[key].send(JSON.stringify({'author':arrmess['author'],'message':arrmess['text']}));
      }
    }
  });

  ws.on('close', function() {
    console.log('соединение закрыто ' + id);
    delete clients[id];
  });

});


// обычный сервер (статика) на порту 8080
var fileServer = new Static.Server('.');
http.createServer(function (req, res) {
  
  fileServer.serve(req, res);

}).listen(8080);

console.log("Сервер запущен на портах 8080, 8081");

