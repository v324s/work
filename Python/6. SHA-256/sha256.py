import hashlib

str = "123123"
# преобразовать строку в байты
str = str.encode('utf-8')
# создать объект хеша SHA-256
hash_object = hashlib.sha256(str)
# обновить объект хеша оригинальным паролем
hash_object.update(str)

# вычислить SHA-256 хеш из обновленного объекта хеша
hex_dig = hash_object.hexdigest()
# вывести хеш на экран
print(hex_dig)