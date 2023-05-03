<?php
$err=0;
$answer_text='';

$data = json_decode(file_get_contents('php://input'), true);
if ($data['id'] && $data['username']){
    try {
        $dbh = new PDO('mysql:dbname=idkmn_tg;host=127.0.0.1', 'root', 'root');
    } catch (PDOException $e) {
        die($e->getMessage());
    }
    $sql="SELECT id,uid FROM `users` WHERE uid=:uid";
    $sth=$dbh->prepare($sql);
    $sth->execute(array('uid' => $data['id']));
    $row = $sth->fetchAll();
    if (count($row)>0){
        $err=1;
        $answer_text='Пользователь уже зарегистрирован';
    }else{
        $sql="INSERT INTO `users`(
            `uid`, 
            `first_name`, 
            `last_name`, 
            `username`, 
            `is_bot`, 
            `language_code`, 
            `can_join_groups`, 
            `can_read_all_group_messages`, 
            `supports_inline_queries`, 
            `is_premium`, 
            `added_to_attachment_menu`, 
            `reg_date`
            ) VALUES (
                :uid, 
                :first_name, 
                :last_name, 
                :username, 
                :is_bot, 
                :language_code, 
                :can_join_groups, 
                :can_read_all_group_messages, 
                :supports_inline_queries, 
                :is_premium, 
                :added_to_attachment_menu, 
                :rd)";
        $sth=$dbh->prepare($sql);
        $rd=getTime();
        $sth->execute(array(
            'uid' => $data['id'],
            'first_name' => $data['first_name'] ? $data['first_name'] : 'NULL',
            'last_name' => $data['last_name'] ? $data['first_name'] : 'NULL',
            'username' => $data['username'],
            'is_bot' => !$data['is_bot'] ? 'false' : 'true',
            'language_code' => $data['language_code'],
            'can_join_groups' => $data['can_join_groups'] ? $data['can_join_groups'] : 'NULL',
            'can_read_all_group_messages' => $data['can_read_all_group_messages'] ? $data['can_read_all_group_messages'] : 'NULL',
            'supports_inline_queries' => $data['supports_inline_queries'] ? $data['supports_inline_queries'] : 'NULL',
            'is_premium' => $data['is_premium'] ? $data['is_premium'] : 'NULL',
            'added_to_attachment_menu' => $data['added_to_attachment_menu'] ? $data['added_to_attachment_menu'] : 'NULL',
            'rd' => $rd
            ));
        $sqk="INSERT INTO `acc`(`uid`) VALUES (:uid)";
        $sth=$dbh->prepare($sql);
        $sth->execute(array('uid' => $data['id']));
        $answer_text=$data['username'].' - registered';
    }
    $ans=['errorCode' => $err,'msg'=>$answer_text, 'mktime'=> getTime()];
    echo json_encode($ans);
}

function getTime()
{
    return date('Y-m-d\TH:i:s\Z');;
}
?>