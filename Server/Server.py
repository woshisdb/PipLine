import socket
import json

def start_server():
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind(('127.0.0.1', 8080))  # 绑定IP和端口
    server_socket.listen(1)  # 设置最多一个连接
    print("等待 Unity 连接...")

    while True:
        client_socket, addr = server_socket.accept()
        print(f"连接来自: {addr}")

        # 接收来自客户端的数据
        data = client_socket.recv(1024).decode('utf-8')
        if data:
            print(f"从 Unity 接收到的数据: {data}")

            # 将接收到的字符串转换为 JSON 对象
            json_data = json.loads(data)
            print(f"转换后的 JSON 数据: {json_data}")

            # 回复客户端消息
            response_data = {"status": "success", "message": "收到 JSON 数据"}
            client_socket.send(json.dumps(response_data).encode('utf-8'))

        # 关闭连接
        client_socket.close()

if __name__ == '__main__':
    start_server()
