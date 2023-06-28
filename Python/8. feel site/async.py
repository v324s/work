import asyncio
import aiohttp

# задаем ссылку на API
api_url = "http://fuji.ru/api/v1/"

# открываем файл с названиями методов
with open("C:/Users/IT/Desktop/python/tester/words.txt", "r") as f:
    # считываем все строки из файла
    words = f.readlines()

# создаем пустой список для хранения ссылок
links = []

async def check_word(word):
    # удаляем символ переноса строки в конце строки
    word = word.strip()
    # формируем ссылку на метод
    url = api_url + word
    async with aiohttp.ClientSession(timeout=aiohttp.ClientTimeout(total=60)) as session:
        async with session.get(url) as response:
            # проверяем статус-код ответа
            if response.status == 200:
                # если статус-код 200, то добавляем ссылку в список
                links.append(url)
                # сохраняем ссылку в отдельный файл
                with open("links.txt", "a") as f:
                    f.write(url + "\n")
            # выводим результат запроса в консоль
            print(f"{word} - {response.status}")

async def main():
    # создаем список задач для проверки каждого метода
    tasks = [asyncio.create_task(check_word(word)) for word in words]
    # ждем завершения всех задач
    await asyncio.gather(*tasks)

# запускаем асинхронную программу
asyncio.run(main())