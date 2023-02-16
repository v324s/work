import telebot
import json
import requests
import os
import datetime
from telebot import types

bot = telebot.TeleBot('')

commandAndAnswer={
    "/start": "👋🤖 \nПППривет.\nЯ - РРРобот\nЯ буду тебя ибац.\nБип-бип-БАП"
}

def logDialog(uid, text, bot):
    now = datetime.datetime.now()
    print(now)
    if os.path.isdir('dialogs') == 0:
        os.makedirs('dialogs')
    if os.path.isfile("dialogs/"+str(uid)+".txt") == 0:
        txt_file = open("dialogs/"+str(uid)+".txt", "w+", encoding="utf-8")
    else:
        txt_file = open("dialogs/"+str(uid)+".txt", "a+", encoding="utf-8")
    if bot:
        sender="bot"
    else:
        sender="user"
    txt_file.write("[{}] {}: {}\n".format(now, sender, text))
    txt_file.close()

def sendMessage(uid, text):
    logDialog(uid, text, 1)
    bot.send_message(uid, text)

@bot.message_handler(commands=['start'])
def start(message):
    logDialog(message.from_user.id,message.text,0)
    markup = types.ReplyKeyboardMarkup(resize_keyboard=True)
    btn1 = types.KeyboardButton("👋 Поздороваться")
    btn2 = types.KeyboardButton("👤 Профиль")
    btn3 = types.KeyboardButton("👪 Семья")
    btn4 = types.KeyboardButton("🧑‍💼 Работа")
    btn5 = types.KeyboardButton("🏡🚗 Имущество")
    btn6 = types.KeyboardButton("/start")
    markup.add(btn1,btn2,btn3,btn4,btn5,btn6)
    r=requests.post("http://localhost/idkmn/method/reg.php", json={
        "id": message.from_user.id,
        "is_bot": message.from_user.is_bot,
        "first_name": message.from_user.first_name,
        "last_name": message.from_user.last_name,
        "username": message.from_user.username,
        "language_code": message.from_user.language_code,
        "can_join_groups": message.from_user.can_join_groups,
        "can_read_all_group_messages": message.from_user.can_read_all_group_messages,
        "supports_inline_queries": message.from_user.supports_inline_queries,
        "is_premium": message.from_user.is_premium,
        "added_to_attachment_menu": message.from_user.added_to_attachment_menu
        })
    print(json.loads(r.text))
    sendMessage(message.from_user.id, commandAndAnswer["/start"])
    # logDialog(message.from_user.id,commandAndAnswer["/start"],1)
    # bot.send_message(message.from_user.id, commandAndAnswer["/start"], reply_markup=markup)

@bot.message_handler(content_types="web_app_data") #получаем отправленные данные 
def answer(webAppMes):
   print(webAppMes) #вся информация о сообщении
   print(webAppMes.web_app_data.data) #конкретно то что мы передали в бота
   bot.send_message(webAppMes.chat.id, f"получили инофрмацию из веб-приложения: {webAppMes.web_app_data.data}") 
   #отправляем сообщение в ответ на отправку данных из веб-приложения 

@bot.message_handler(content_types=['text'])
def get_text_messages(message):


    print(message.text)

    if message.text == '👋 Поздороваться':
        markup = types.ReplyKeyboardMarkup(resize_keyboard=True) #создание новых кнопок
        btn1 = types.KeyboardButton('Как стать автором на Хабре?')
        btn2 = types.KeyboardButton('Правила сайта')
        btn3 = types.KeyboardButton('Советы по оформлению публикации')
        markup.add(btn1, btn2, btn3)
        bot.send_message(message.from_user.id, '❓ Задайте интересующий вас вопрос', reply_markup=markup) #ответ бота

    elif message.text == '👤 Профиль':
        r=requests.post("http://localhost/idkmn/method/profile.php", json={"id": message.from_user.id})
        r=json.loads(r.text)
        print(r['msg'])
        bot.reply_to(message, "\n👤 id: "+r['msg']['uid']+"\n\n🏳️ Статус: "+r['msg']['status']+"\n\n🎮 Кол-во игр: "+r['msg']['games']+"\n\n🪙 Монет: "+r['msg']['coins'])

    elif message.text == 'Как стать автором на Хабре?':
        bot.send_message(message.from_user.id,"qq", parse_mode='Markdown')
        print(message.from_user)

    elif "Напиши" in message.text:
        msg=message.text.split("@")
        r=requests.get("http://localhost/idkmn/method/getUID.php", json={"username": msg[1]})
        r=json.loads(r.text)
        print(r['msg'])
        if r['errorCode'] == 0:
            bot.send_message(r['msg'], 'Приветик') #ответ бота
            bot.send_message(message.from_user.id, 'написали этому человеку') #ответ бота

    elif message.text == 'Советы по оформлению публикации':
        bot.send_message(message.from_user.id, 'Подробно про советы по оформлению публикаций прочитать по ' + '[ссылке](https://habr.com/ru/docs/companies/design/)', parse_mode='Markdown')

@bot.message_handler(func=lambda m: True)
def echo_all(message):
	bot.reply_to(message, message.text)


bot.polling(none_stop=True, interval=0) #обязательная для работы бота часть




