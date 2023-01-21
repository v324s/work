eval(Base64.decode(some_achiv_str));

for (let i=0; i< this_anime_achievements.length;i++){ 
    $.post('https://jut.su/engine/ajax/get_achievement.php',{
        'achiv_id': this_anime_achievements[i]['id'],
        'achiv_hash': this_anime_achievements[i]['hash'],
        'the_login_hash': the_login_hash}, function (e){console.log(e)});
}