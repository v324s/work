import random
import socket
import struct

print(socket.inet_ntoa(struct.pack('>I', random.randint(1, 0xffffffff))))
print(socket.inet_ntoa(struct.pack('>I', random.randint(1, 0xffffffff))))