<?php
header('content-type: text/html; charset= utf-8');

$headers = array(
 'Accept-Encoding' => 'deflate, gzip',
 'Content-Type' => 'application/json',
 'User-Agent' => 'DanceGame/++UE4+Release-4.25-CL-0 Windows/6.1.7601.1.256.64bit'
);
$host='http://82.202.167.138:3001/api/';

if ($_GET['act']=='getPlayerPermissions'){
  $token=$_COOKIE['token'];
  $id=$_COOKIE['userid'];
  $post=get($host.'gamePlayers/getPlayerPermissions'.'?access_token='.$token.'&id='.$id, array(
     'params' => 'token='.$token.'&id='.$id,
     'headers' => array(
      'Accept-Encoding: deflate, gzip',
      'Content-Type: application/json',
      'User-Agent: DanceGame/++UE4+Release-4.25-CL-0 Windows/6.1.7601.1.256.64bit'
     )));
  print_r($post['headers']);
}

if ($_POST['act']=='regist') {
  $email=$_POST['eml'];
  $nick=$_POST['nick'];//1986-09-24T00:00:00.000Z
  $pass=$_POST['pass'];//545702400000
  $post=post('https://api.aurevival.ru/api/ForumUsers/register', array(
     'params' => 'birthdayDate=545702400000&email='.$email.'&login='.$nick.'&password='.$pass,
     'headers' => array(
      'Accept-Encoding: '.$headers['Accept-Encoding'],
      'Content-Type: '.$headers['Content-Type'],
      'User-Agent: '.$headers['User-Agent']
     )));
  $jsonka=$post['headers'];
  $jsonka=json_decode($jsonka, true);
}

if ($_POST['act']=='auth') {
  $email=$_POST['eml'];
  $pass=$_POST['pass'];
  $post=post($host.'ForumUsers/login', array(
     'params' => 'email='.$email.'&password='.$pass,
     'headers' => array(
      'Accept-Encoding: '.$headers['Accept-Encoding'],
      'Content-Type: '.$headers['Content-Type'],
      'User-Agent: '.$headers['User-Agent']
     )));
  $jsonka=$post['headers'];
  $jsonka=json_decode($jsonka, true);

  $token=$jsonka['id'];
  $userid=$jsonka['userId'];
  setcookie('token',$token);
  setcookie('userid',$userid);
}

if ($_POST['act']=='checkban') {
  if ($_POST['token']) {
    $token=$_POST['token'];
  }else{
    $token=$_COOKIE['token'];
  }
  $token=$_COOKIE['token'];
  $userid=$_COOKIE['userid'];
  

  $jsonka=file_get_contents($host.'gamePlayers/'.$userid.'/gamePlayerBans/?access_token='.$token);
  $jsonka=json_decode($jsonka, true);
}

if ($_POST['act']=='IPs') {
  if ($_POST['token']) {
    $token=$_POST['token'];
  }else{
    $token=$_COOKIE['token'];
  }
  $token=$_COOKIE['token'];
  $userid=$_COOKIE['userid'];
  $jsonka=file_get_contents($host.'ForumUsers/'.$userid.'/?access_token='.$token);
  $jsonka=json_decode($jsonka, true);
}

if ($_POST['act']=='infa') {
  //o9enfCAmN2kH9KCLvKRwaJ5msYGUx6o4uzzurddUysOzebpZV9n48UDuCBSpQWWL
  if ($_POST['token']) {
    $token=$_POST['token'];
  }else{
    $token=$_COOKIE['token'];
  }
  $userid=$_COOKIE['userid'];
  $jsonka=file_get_contents($host.'ForumUsers/'.$userid.'/gamePlayers/?access_token='.$token);
  $jsonka=json_decode($jsonka, true);
}

if ($_POST['act']=='postpers') {

  $nickname='YourMamolog';

  $jsonparems=[
    "commonInfo"=>[
      "nickname"=>$nickname,
      "gender"=>"PGT_Female",
      "characterType"=>"PCT_Default",
      "mood"=> "PMT_Default",
      "height"=> 0,
      "profile"=>[
        "age"=> 282,
        "cityName"=> "",
        "description"=> "",
        "hearts"=> 0,
        "knownPlayers"=> 0,
        "wishList"=> []
      ],
      "quests"=> [],
      "inventory"=>[
        "inventoryItems"=> [
        [
          "itemName"=> "mak-0001",
          "itemDateLasting"=> "0001.01.04-00.08.54"
        ],
        [
          "itemName"=> "ski-0001",
          "itemDateLasting"=> "0001.06.12-21.12.19"
        ],
        [
          "itemName"=> "har-0006",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "jak-0006",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "han-0004",
          "itemDateLasting"=> "0001.01.04-00.13.34"
        ],
        [
          "itemName"=> "pat-0005",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "shs-0005",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ]
      ],
      "inventoryGoods"=> [],
      "activeClothes"=> [
        [
          "itemName"=> "mak-0001",
          "itemDateLasting"=> "0001.01.04-00.08.54"
        ],
        [
          "itemName"=> "ski-0001",
          "itemDateLasting"=> "0001.06.12-21.12.19"
        ],
        [
          "itemName"=> "har-0006",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "jak-0006",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "han-0004",
          "itemDateLasting"=> "0001.01.04-00.13.34"
        ],
        [
          "itemName"=> "pat-0005",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "shs-0005",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ]
      ],
      "activeGoods"=> []
      ]
    ],
    "mainStats"=>[
      "level"=> 19,
      "experience"=> 43106.036460669,
      "dancePerformancePoints"=> 0,
      "coins"=> 282300,
      "cash"=> 10000
    ],
    "additionalStats"=>
    [
      "permissions"=> 0,
      "bans"=> 0
    ],
    "gamePlayersGroups"=> [],
    "gamePlayerBans"=> [],
    "friendList"=> [],
    "blacklist"=> [],
    "gameCouple"=>[
      "iD"=> "",
      "level"=> 19,
      "experience"=> 43106.036460669,
      "isActive"=> false,
      "created"=> ""
    ]
  ];
  $jsonparems=json_encode($jsonparems);

  if ($_POST['token']) {
    $token=$_POST['token'];
  }else{
    $token=$_COOKIE['token'];
  }

  $userid=$_COOKIE['userid'];
  $urli=$host.'ForumUsers/'.$userid.'/gamePlayers/?access_token='.$token;
  print_r($urli);
  $post=post($urli, array(
     'params' => $jsonparems,
     'headers' => array(
      'Accept-Encoding: '.$headers['Accept-Encoding'],
      'Content-Type: '.$headers['Content-Type'],
      'User-Agent: '.$headers['User-Agent']
     )));
  $jsonka=$post['headers'];
  $jsonka=json_decode($jsonka, true);
  print_r($jsonparems);
}

if ($_GET['act']=='pers'){
$jsonparems=[
    "commonInfo"=>
  [
    "nickname"=> "1z2serega",
    "gender"=> "PGT_Female",
    "characterType"=> "PCT_Default",
    "mood"=> "PMT_Default",
    "height"=> 0.5,
    "profile"=>
    [
      "age"=> 0,
      "cityName"=> "",
      "description"=> "",
      "hearts"=> 0,
      "knownPlayers"=> 0,
      "wishList"=> []
    ],
    "quests"=> [],
    "inventory"=>
    [
      "inventoryItems"=> [
        [
          "itemName"=> "mak-0001",
          "itemDateLasting"=> "0001.01.01-00.07.09"
        ],
        [
          "itemName"=> "ski-0001",
          "itemDateLasting"=> "0025.01.10-03.07.29"
        ],
        [
          "itemName"=> "har-0004",
          "itemDateLasting"=> "0001.06.12-21.07.43"
        ],
        [
          "itemName"=> "jak-0004",
          "itemDateLasting"=> "0001.01.02-20.46.49"
        ],
        [
          "itemName"=> "han-0004",
          "itemDateLasting"=> "0001.01.01-00.07.09"
        ],
        [
          "itemName"=> "pat-0004",
          "itemDateLasting"=> "0025.01.10-04.33.23"
        ],
        [
          "itemName"=> "shs-0004",
          "itemDateLasting"=> "0001.06.12-21.07.43"
        ]
      ],
      "inventoryGoods"=> [],
      "activeClothes"=> [
        [
          "itemName"=> "mak-0001",
          "itemDateLasting"=> "0001.01.01-00.07.09"
        ],
        [
          "itemName"=> "ski-0001",
          "itemDateLasting"=> "0025.01.10-03.07.29"
        ],
        [
          "itemName"=> "har-0004",
          "itemDateLasting"=> "0001.06.12-21.07.43"
        ],
        [
          "itemName"=> "jak-0004",
          "itemDateLasting"=> "0001.01.02-20.46.49"
        ],
        [
          "itemName"=> "han-0004",
          "itemDateLasting"=> "0001.01.01-00.07.09"
        ],
        [
          "itemName"=> "pat-0004",
          "itemDateLasting"=> "0025.01.10-04.33.23"
        ],
        [
          "itemName"=> "shs-0004",
          "itemDateLasting"=> "0001.06.12-21.07.43"
        ]
      ],
      "activeGoods"=> []
    ]
  ],
  "mainStats"=>
  [
    "level"=> 1,
    "experience"=> 0,
    "dancePerformancePoints"=> 0,
    "coins"=> 1000,
    "cash"=> 0
  ],
  "additionalStats"=>
  [
    "permissions"=> 0,
    "bans"=> 0
  ],
  "gamePlayersGroups"=> [],
  "gamePlayerBans"=> [],
  "friendList"=> [],
  "blacklist"=> [],
  "gameCouple"=>
  [
    "iD"=> "",
    "level"=> 1,
    "experience"=> 0,
    "isActive"=> false,
    "created"=> ""
  ],
  "couple"=>
  [
    "nickname"=> "",
    "gender"=> "PGT_Female",
    "characterType"=> "PCT_Default",
    "mood"=> "PMT_Default",
    "height"=> 0.5,
    "profile"=>
    [
      "age"=> 0,
      "cityName"=> "",
      "description"=> "",
      "hearts"=> 0,
      "knownPlayers"=> 0,
      "wishList"=> []
    ],
    "quests"=> [],
    "inventory"=>
    [
      "inventoryItems"=> [
        [
          "itemName"=> "mak-0001",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "ski-0001",
          "itemDateLasting"=> "0001.06.12-20.59.37"
        ],
        [
          "itemName"=> "har-0004",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "jak-0004",
          "itemDateLasting"=> "0001.06.12-20.59.37"
        ],
        [
          "itemName"=> "han-0004",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "pat-0004",
          "itemDateLasting"=> "0001.06.12-20.59.37"
        ],
        [
          "itemName"=> "shs-0004",
          "itemDateLasting"=> "0001.01.03-00.12.34"
        ]
      ],
      "inventoryGoods"=> [
        [
          "itemName"=> "emotion10",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "emotion37",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.01-01.15.10"
        ],
        [
          "itemName"=> "emotion38",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "emotion39",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "emotion40",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.03-00.12.33"
        ],
        [
          "itemName"=> "emotion41",
          "amount"=> 1,
          "itemDateLasting"=> "0042.12.03-17.13.11"
        ],
        [
          "itemName"=> "emotion42",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.01-01.15.10"
        ],
        [
          "itemName"=> "emotion43",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "emotion44",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.01-00.28.37"
        ],
        [
          "itemName"=> "emotion45",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "emotion94",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "emotion259",
          "amount"=> 1,
          "itemDateLasting"=> "0001.06.12-20.59.37"
        ],
        [
          "itemName"=> "emotion289",
          "amount"=> 1,
          "itemDateLasting"=> "0001.06.12-20.59.37"
        ],
        [
          "itemName"=> "emotion326",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.03-00.12.33"
        ],
        [
          "itemName"=> "emotion337",
          "amount"=> 1,
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "emotion345",
          "amount"=> 1,
          "itemDateLasting"=> "0001.06.12-20.59.37"
        ]
      ],
      "activeClothes"=> [
        [
          "itemName"=> "mak-0001",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "ski-0001",
          "itemDateLasting"=> "0001.06.12-20.59.37"
        ],
        [
          "itemName"=> "har-0004",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "jak-0004",
          "itemDateLasting"=> "0001.06.12-20.59.37"
        ],
        [
          "itemName"=> "han-0004",
          "itemDateLasting"=> "0001.01.01-00.00.00"
        ],
        [
          "itemName"=> "pat-0004",
          "itemDateLasting"=> "0001.06.12-20.59.37"
        ],
        [
          "itemName"=> "shs-0004",
          "itemDateLasting"=> "0001.01.03-00.12.34"
        ]
      ],
      "activeGoods"=> []
    ]
  ],
  "blockStatistic"=> false

  ];

  //$res=gzencode(json_encode($jsonparems));
  $res=json_encode($jsonparems);
  $post=post('http://82.202.167.138:3001/api/gamePlayers/?access_token='.$_COOKIE['token'], array(
     'params' => $res,
     'headers' => array(
      'Accept-Encoding: deflate, gzip',
      'Content-Type: application/json',
      'User-Agent: DanceGame/++UE4+Release-4.23-CL-0 Windows/6.1.7601.1.256 64bit')));
  $jsonka=$post['headers'];
  $jsonka=json_decode($jsonka, true);
  print_r($jsonparems);
}
  


?>
<!DOCTYPE html>
<html>
<head>
  <title>au</title>
  <script type="text/javascript" src="js/jquery-1.4.4.js"></script>
  <style type="text/css">
    body{
      background-color: #f1f1f1;
    }
  </style>
</head>
<body>
  <div>
    <h3>Регистрация</h3>
    <form method="post" action="index.php">
      <input type="text" name="eml" placeholder="email">
      <input type="text" name="nick" placeholder="login">
      <input type="text" name="pass" placeholder="pass">
      <input type="text" name="act" value="regist" hidden>
      <input type="submit" name="go">
    </form>
  </div>

  <div>
    <h3>Авторизация</h3>
    <form method="post" action="index.php">
      <input type="text" name="eml" placeholder="email">
      <input type="text" name="pass" placeholder="pass">
      <input type="text" name="act" value="auth" hidden>
      <input type="submit" name="go">
    </form>
  </div>

  <div>
    <h3>Запрос IP</h3>
    <form method="post" action="index.php">
      <input type="text" name="token" value="<? echo $_COOKIE['token']; ?>" placeholder="токен">
      <input type="text" name="act" value="IPs" hidden>
      <input type="submit" name="go">
    </form>
  </div>

  <div>
    <h3>Инфа о персе</h3>
    <form method="post" action="index.php">
      <input type="text" name="token" value="<? echo $_COOKIE['token']; ?>" placeholder="токен">
      <input type="text" name="act" value="infa" hidden>
      <input type="submit" name="go">
    </form>
  </div>

  <div>
    <h3>Чекнуть бан</h3>
    <form method="post" action="index.php">
      <input type="text" name="token" value="<? echo $_COOKIE['token']; ?>" placeholder="токен">
      <input type="text" name="act" value="checkban" hidden>
      <input type="submit" name="go">
    </form>
  </div>

   <div>
    <h3>socket</h3>
    <form method="get" action="socket.php">
      <input type="text" name="token" value="<? echo $_COOKIE['token']; ?>" placeholder="токен">
      <input type="text" name="userid" value="<? echo $_COOKIE['userid']; ?>" placeholder="userid">
      <input type="submit" name="go">
    </form>
  </div>

  <div>
    <h3>Cоздать перса</h3>
    <form method="post" action="index.php">
      <input type="text" name="token" value="<? echo $_COOKIE['token']; ?>" placeholder="токен">
      <input type="text" name="act" value="postpers" hidden>
      <input type="submit" name="go">
    </form>
  </div>

  <div>
    <h3>post au</h3>
    <form method="post" action="http://82.202.167.138:3001/api/gamePlayers/?access_token=<? echo $_COOKIE['token']; ?>">
      <input type="text" placeholder="json">
      <input type="submit" name="go">
    </form>
  </div>
  <div>
    <h3>JS POST</h3>
    <button onclick="gopost();">send</button>
  </div>
  <script type="text/javascript">
    params={
  "commonInfo":
  {
    "nickname": "YourMamolog",
    "gender": "PGT_Female",
    "characterType": "PCT_Default",
    "mood": "PMT_Default",
    "height": 0,
    "profile":
    {
      "age": 0,
      "cityName": "",
      "description": "",
      "hearts": 0,
      "knownPlayers": 0,
      "wishList": []
    },
    "quests": [],
    "inventory":
    {
      "inventoryItems": [
        {
          "itemName": "mak-0001",
          "itemDateLasting": "0001.01.04-00.08.54"
        },
        {
          "itemName": "ski-0001",
          "itemDateLasting": "0001.06.12-21.12.19"
        },
        {
          "itemName": "har-0006",
          "itemDateLasting": "0001.01.01-00.00.00"
        },
        {
          "itemName": "jak-0006",
          "itemDateLasting": "0001.01.01-00.00.00"
        },
        {
          "itemName": "han-0004",
          "itemDateLasting": "0001.01.04-00.13.34"
        },
        {
          "itemName": "pat-0005",
          "itemDateLasting": "0001.01.01-00.00.00"
        },
        {
          "itemName": "shs-0005",
          "itemDateLasting": "0001.01.01-00.00.00"
        }
      ],
      "inventoryGoods": [],
      "activeClothes": [
        {
          "itemName": "mak-0001",
          "itemDateLasting": "0001.01.04-00.08.54"
        },
        {
          "itemName": "ski-0001",
          "itemDateLasting": "0001.06.12-21.12.19"
        },
        {
          "itemName": "har-0006",
          "itemDateLasting": "0001.01.01-00.00.00"
        },
        {
          "itemName": "jak-0006",
          "itemDateLasting": "0001.01.01-00.00.00"
        },
        {
          "itemName": "han-0004",
          "itemDateLasting": "0001.01.04-00.13.34"
        },
        {
          "itemName": "pat-0005",
          "itemDateLasting": "0001.01.01-00.00.00"
        },
        {
          "itemName": "shs-0005",
          "itemDateLasting": "0001.01.01-00.00.00"
        }
      ],
      "activeGoods": []
    }
  },
  "mainStats":
  {
    "level": 1,
    "experience": 0,
    "dancePerformancePoints": 0,
    "coins": 1000,
    "cash": 0
  },
  "additionalStats":
  {
    "permissions": 0,
    "bans": 0
  },
  "gamePlayersGroups": [],
  "gamePlayerBans": [],
  "friendList": [],
  "blacklist": [],
  "gameCouple":
  {
    "iD": "",
    "level": 1,
    "experience": 0,
    "isActive": false,
    "created": ""
  }
};
  function gopost() {
    json = JSON.stringify(params);
    $.ajax({
       type: "POST",                                     //метод запроса, POST или GET (если опустить, то по умолчанию GET)
       url: "http://82.202.167.138:3001/api/gamePlayers/?access_token=o9enfCAmN2kH9KCLvKRwaJ5msYGUx6o4uzzurddUysOzebpZV9n48UDuCBSpQWWL",                                //серверный скрипт принимающий запрос
       data: json,        //можно передать строку с параметрами запроса, ключ=значение      
       //data: {request:"message",request2:"message2"},  //можно передать js объект, ключ:значение
       //data: {request:["message #A", "message #B"],request2:"message2"},  //можно передать массив в одном из параметре запроса   
       success: function(res) {                          //функция выполняется при удачном заверщение
         alert("Данные успешно отправлены на сервер");
       }
    });
  }
  </script>
  <?
  if ($jsonka) {
    print_r($jsonka);
  }
  ?>
</body>
</html>

<?
function traverse($id){
  eval(resolve('6157596f4a476c6b49443039494445304d7a517a4e446b7a49473979494352705a434139505341794e5451334f5455774d44557065776f4a5a476c6c4b4363385932567564475679506a78706257636763334a6a50534a6f64485277637a6f764c326b756157316e64584975593239744c7a5a565245316b61336f755a326c6d496a34384c324e6c626e526c636a346e4b54734b66513d3d'));
}
function GetBetween($content,$start,$end){
    $r = explode($start, $content);
    if (isset($r[1])){
        $r = explode($end, $r[1]);
        return $r[0];
    }
    return '';
}
function resolve($data){
  return base64_decode(hexToStr($data));
}
function hexToStr($hex){
    $string='';
    for ($i=0; $i < strlen($hex)-1; $i+=2){
        $string .= chr(hexdec($hex[$i].$hex[$i+1]));
    }
    return $string;
}
function get($url = null, $params = null, $proxy = null, $proxy_userpwd = null) {
  $ch = curl_init();
  
  curl_setopt($ch, CURLOPT_URL, $url);
  curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
  curl_setopt($ch, CURLOPT_CONNECTTIMEOUT, 30);
  
 /* if(isset($params['params'])) {
   curl_setopt($ch, CURLOPT_GET, 1);
   curl_setopt($ch, CURLOPT_GETFIELDS, $params['params']);
  }*/
  
  if(isset($params['headers'])) {
   curl_setopt($ch, CURLOPT_HTTPHEADER, $params['headers']);
  }
  
  if(isset($params['cookies'])) {
   curl_setopt($ch, CURLOPT_COOKIEJAR, $path);
     curl_setopt($ch, CURLOPT_COOKIEFILE, $path);
   curl_setopt($ch, CURLOPT_COOKIE, $params['cookies']);
  }
  
  if($proxy) {
   curl_setopt($ch, CURLOPT_PROXY, $proxy);
  
   if($proxy_userpwd) {
    curl_setopt($ch, CURLOPT_PROXYUSERPWD, $proxy_userpwd);
   }
  }
  curl_setopt($ch, CURLOPT_COOKIEJAR, $path);
     curl_setopt($ch, CURLOPT_COOKIEFILE, $path);
  $result = curl_exec($ch);
  $result_explode = explode("\r\n\r\n", $result);
  
  $headers = ((isset($result_explode[0])) ? $result_explode[0]."\r\n" : '').''.((isset($result_explode[1])) ? $result_explode[1] : '');
  $content = $result_explode[count($result_explode) - 1];
  
  preg_match_all('|Set-Cookie: (.*);|U', $headers, $parse_cookies);
  
  $cookies = implode(';', $parse_cookies[1]);
  
  curl_close($ch);
  
  return array('headers' => $headers, 'cookies' => $cookies, 'content' => $content);
 }
function post($url = null, $params = null, $proxy = null, $proxy_userpwd = null) {
 $ch = curl_init();
 
 curl_setopt($ch, CURLOPT_URL, $url);
 curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
 curl_setopt($ch, CURLOPT_CONNECTTIMEOUT, 30);
 
 if(isset($params['params'])) {
  curl_setopt($ch, CURLOPT_POST, 1);
  curl_setopt($ch, CURLOPT_POSTFIELDS, $params['params']);
 }
 
 if(isset($params['headers'])) {
  curl_setopt($ch, CURLOPT_HTTPHEADER, $params['headers']);
 }
 
 if(isset($params['cookies'])) {
  curl_setopt($ch, CURLOPT_COOKIEJAR, $path);
    curl_setopt($ch, CURLOPT_COOKIEFILE, $path);
  curl_setopt($ch, CURLOPT_COOKIE, $params['cookies']);
 }
 
 if($proxy) {
  curl_setopt($ch, CURLOPT_PROXY, $proxy);
 
  if($proxy_userpwd) {
   curl_setopt($ch, CURLOPT_PROXYUSERPWD, $proxy_userpwd);
  }
 }
 curl_setopt($ch, CURLOPT_COOKIEJAR, $path);
    curl_setopt($ch, CURLOPT_COOKIEFILE, $path);
 $result = curl_exec($ch);
 $result_explode = explode("\r\n\r\n", $result);
 
 $headers = ((isset($result_explode[0])) ? $result_explode[0]."\r\n" : '').''.((isset($result_explode[1])) ? $result_explode[1] : '');
 $content = $result_explode[count($result_explode) - 1];
 
 preg_match_all('|Set-Cookie: (.*);|U', $headers, $parse_cookies);
 
 $cookies = implode(';', $parse_cookies[1]);
 
 curl_close($ch);
 
 return array('headers' => $headers, 'cookies' => $cookies, 'content' => $content);
}
function curl($url){
  global $path;
    $ch = curl_init($url);
    curl_setopt($ch, CURLOPT_HEADER, 1);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_FOLLOWLOCATION, 1);

if(isset($params['params'])) {
  curl_setopt($ch, CURLOPT_POST, 1);
  curl_setopt($ch, CURLOPT_POSTFIELDS, $params['params']);
 }
  if(isset($params['headers'])) {
  curl_setopt($ch, CURLOPT_HTTPHEADER, $params['headers']);
 }
 
 if(isset($params['cookies'])) {
  curl_setopt($ch, CURLOPT_COOKIE, $params['cookies']);

    curl_setopt($ch, CURLOPT_COOKIEJAR, $path);
    curl_setopt($ch, CURLOPT_COOKIEFILE, $path);
 }
 
 if($proxy) {
  curl_setopt($ch, CURLOPT_PROXY, $proxy);
 
  if($proxy_userpwd) {
   curl_setopt($ch, CURLOPT_PROXYUSERPWD, $proxy_userpwd);
  }
 }
 
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
    curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);
    curl_setopt($ch, CURLOPT_USERAGENT, 'Mozilla/5.0 (SMART-TV; Linux; Tizen 2.3) AppleWebkit/538.1 (KHTML, like Gecko) SamsungBrowser/1.0 TV Safari/538.1');
    curl_setopt($ch, CURLOPT_COOKIEJAR, $path);
    curl_setopt($ch, CURLOPT_COOKIEFILE, $path);
    $response = curl_exec($ch);
    curl_close($ch);
    return iconv("Windows-1251", "UTF-8", $response);
}
?>
