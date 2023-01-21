<?php
header('content-type: text/html; charset= utf-8');

$headers = array(
 'Accept-Encoding' => 'json',
 'Content-Type' => 'application/x-www-form-urlencoded',
 'User-Agent' => 'VKAndroidApp/5.12-2353 (Android 9.0; SDK 23; armeabi-v7a; Vertu; ru)'
);

$host='http://84.38.177.70:3001/api/';

$tok='fAUUqpbo5CNDrcbnBn8L566vjoNtNFYM1NrLA1Kt8mimRoaPWTbf9O9ihAZgsAzK';
  $req=file_get_contents('http://84.38.177.70:3001/api/OnlinePlayers/?access_token='.$tok);
  $req=json_decode($req,true );
  $text="";
 $first=true;
  for ($i=0; $i < count($req) ; $i++) {
    $lvl=explode('.', $req[$i]['level']); 
      if ($first) {
        $text=$req[$i]['nickname'].'(lvl - '.$lvl[0].' | Room - '.$req[$i]['roomNumber'].' | Club - '.$req[$i]['clubName'].')'."\r\n";
        $first=false;
      }else{
        $text.=$req[$i]['nickname'].'(lvl - '.$lvl[0].' | Room - '.$req[$i]['roomNumber'].' | Club - '.$req[$i]['clubName'].')'."\r\n";
      } 
  }
  /*if (strlen($text)>2000) {
              $skokchastey=strlen($text)/2000;
            }else{
              $msg = $text;
              SendMessage($msg,$user_id); 
            }
            if ($skokchastey>0) {
              $msg = str_split($text, round(strlen($text) / $skokchastey));

              for ($i=0; $i < count($msg) ; $i++) { 
                SendMessage($msg[$i],$user_id); 
                sleep(1);
              } 
            }*/
   print_r($text);
/* print_r($result);
//print_r($result[1]);
 $first=true;
 foreach ($massclubs as $key => $value) {
  if ($massclubs[$key]!="") {
    if ($first) {
      $massclubsshort=$massclubs[$key].'|';
      $first=false;
    }else{
      $massclubsshort.=$massclubs[$key].'|';
    }
  }
 }
 $massclubsshort=explode('|', $massclubsshort);
 print_r($massclubsshort);
foreach ($result as $key => $value) {
  echo $result[$key].'<br>';
}
*/




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
  
  if(isset($params['params'])) {
   curl_setopt($ch, CURLOPT_GET, 1);
   curl_setopt($ch, CURLOPT_GETFIELDS, $params['params']);
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
