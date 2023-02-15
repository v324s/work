# кнопка под сообщением

@bot.message_handler(commands = ['start'])
def url(message):
    markup = types.InlineKeyboardMarkup()
    btn1 = types.InlineKeyboardButton(text='Наш сайт', url='https://habr.com/ru/all/') #####
    markup.add(btn1)
    bot.send_message(message.from_user.id, "По кнопке ниже можно перейти на сайт хабра", reply_markup = markup)


# Клавиатурные кнопки

@bot.message_handler(commands=['start'])
def start(message):

    markup = types.ReplyKeyboardMarkup(resize_keyboard=True)
    btn1 = types.KeyboardButton("🇷🇺 Русский") #####
    btn2 = types.KeyboardButton('🇬🇧 English') #####
    markup.add(btn1, btn2) #####
    bot.send_message(message.from_user.id, "🇷🇺 Выберите язык / 🇬🇧 Choose your language", reply_markup=markup)
