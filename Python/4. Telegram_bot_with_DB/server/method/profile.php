<?php
$err=0;
$answer_text='';

$data = json_decode(file_get_contents('php://input'), true);
if ($data['id']){
    try {
        $dbh = new PDO('mysql:dbname=idkmn_tg;host=127.0.0.1', 'root', 'root');
    } catch (PDOException $e) {
        die($e->getMessage());
    }
    $sql="SELECT * FROM `acc` WHERE uid=:uid";
    $sth=$dbh->prepare($sql);
    $sth->execute(array('uid' => $data['id']));
    $row = $sth->fetch();
    $arr=[
        'uid'=>$row['uid'],
        'coins'=>$row['coins'],
        'games'=>$row['games'],
        'status'=>$row['status']
    ];
    $ans=['errorCode' => $err,'msg'=>$arr];
    echo json_encode($ans);
}
?>