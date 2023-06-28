import requests
import json
import mysql.connector

mydb = mysql.connector.connect(
  host="localhost",
  user="root",
  password="root",
  database="nameDB"
)


url = 'http://localhost/users.json'
response = requests.get(url)
data = response.json()

print("Всего - {0} строк".format(len(data)))

mycursor = mydb.cursor()

sql = "INSERT INTO users (uid, name, phone, birthday, sum, count) VALUES (%s, %s, %s, %s, %s, %s)"

k=0

# # Обрабатываем каждый элемент JSON и добавляем его в базу данных
for item in data:
    val = (item['id'], item['name'], item['phone'], item['birthday'], item['sum'], item['count'])
    mycursor.execute(sql, val)
    k+=1
    print("{0} - {1} Завершено ({2}%)".format(len(data),k,round((k/len(data))*100)))

# # Сохраняем изменения в базе данных
mydb.commit()

print(mycursor.rowcount, "record inserted.")
print("ok")