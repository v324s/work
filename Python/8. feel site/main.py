import requests
import os

# задаем ссылку на API
api_url = "https://fuji.ru/api/v1/"

# открываем файл с названиями методов
with open("C:/Users/IT/Desktop/python/tester/words.txt", "r") as f:
    # считываем все строки из файла
    words = f.readlines()

# создаем пустой список для хранения ссылок
links = []

# перебираем все названия методов
for word in words:
    # удаляем символ переноса строки в конце строки
    word = word.strip()
    # формируем ссылку на метод
    url = api_url + word
    # отправляем GET-запрос по ссылке
    response = requests.get(url)
    # проверяем статус-код ответа
    if response.status_code == 200:
        # если статус-код 200, то добавляем ссылку в список
        links.append(url)
    # выводим результат запроса в консоль
    print(f"{word} - {response.status_code}")

# сохраняем ссылки в отдельный файл
with open("links.txt", "w") as f:
    f.write("\n".join(links))