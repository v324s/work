<?php
$err=0;
$answer_text='';

$data = json_decode(file_get_contents('php://input'), true);
if ($data['username']){
    try {
        $dbh = new PDO('mysql:dbname=idkmn_tg;host=127.0.0.1', 'root', 'root');
    } catch (PDOException $e) {
        die($e->getMessage());
    }
    $sql="SELECT `uid` FROM `users` WHERE username=:uname";
    $sth=$dbh->prepare($sql);
    $sth->execute(array('uname' => $data['username']));
    $row = $sth->fetch();
    if (count($row)>0){
        $answer_text=$row['uid'];
    }
    else{
        $answer_text='Этот пользователь не мой клиент :с';
        $err=2;
    }
    $ans=['errorCode' => $err,'msg'=>$answer_text];
    echo json_encode($ans);
}
?>