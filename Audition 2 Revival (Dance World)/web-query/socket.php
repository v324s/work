<?php
header('content-type: text/html; charset= utf-8');

if ($_GET['token'] && $_GET['userid']) {
  
}
?>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8" />
    <title>Au2 websocket</title>
    <style type="text/css">
      .msgs{
            border: 1px solid;
          padding: 10px;
          width: 98%;
          /* word-spacing: inherit; */
          /* overflow: hidden; */
          word-break: break-all;
      }

    </style>
</head>
<body>
<br>
<br>

Адрес сервера:
<?
if ($_GET['token'] && $_GET['userid']) {
  echo "ws://84.38.177.70:3001/socket.io/?EIO=4&transport=websocket&t=1586549596&id=".$_GET['token']."&userId=".$_GET['userid'];
}
?>
<p>ws://84.38.177.70:3001/socket.io/?EIO=4&transport=websocket&t=1586549596&id=SoHzT4yv3hiQCUPe2oeI03Kr82phbrAmQC3Du1U3gEqXovPf1AfiXntny0RHNvlb&userId=5e90b036acaeb31abae88433</p>
Сообщение
<input id="sockmsg" type="text">
<input id="sock-send-butt" type="button" value="send" onclick="sendmsg();">
<br>
<br>
<input id="sock-recon-butt" type="button" value="reconnect"><input id="sock-disc-butt" type="button" value="disconnect">
<br />
<br />

Полученные сообщения от веб-сокета: 
<div class="msgs" id="sock-info" style="border: 1px solid"> </div>

<script type="text/javascript">
  var socket = new WebSocket(<?

if ($_GET['token'] && $_GET['userid']) {
  echo "\"ws://84.38.177.70:3001/socket.io/?EIO=4&transport=websocket&t=1586549596&id=".$_GET['token']."&userId=".$_GET['userid']."\"";
}

    ?>);


  socket.onopen = function() {
    writeinblock("Соединение установлено.");
    setintrev=setInterval(heartbeat,20000);
  };

  socket.onclose = function(event) {
    if (event.wasClean) {
      writeinblock('Соединение закрыто чисто');
    } else {
      writeinblock('Обрыв соединения'); 
    }
    writeinblock('Код: ' + event.code + ' причина: ' + event.reason);
  };
zag=false;
  socket.onmessage = function(event) {
    writeinblock(event.data);
    if (zag==false) {
      str=event.data;
      str=str.substr(1);
      zag=true;
      sid = JSON.parse(str);
      sid = sid.sid;
    }
  };

  socket.onerror = function(error) {
    writeinblock("Ошибка " + error.message);
  };



function heartbeat() {
  console.log("beat - <? echo $_GET['userid']; ?>");
  socket.send("<? echo $_GET['userid']; ?>");
}
  function sendmsg() {
    socket.send(document.getElementById("sockmsg").value);
  }


  datas=document.getElementById('sock-info');

  function writeinblock(txt) {
    her=datas.innerHTML;
    datas.innerHTML=her+"<br>"+txt;
  }
  

  
  
</script>
</body>
</html>










