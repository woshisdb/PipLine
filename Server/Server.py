import sys
import numpy as np
import matplotlib.pyplot as plt
from matplotlib.backends.backend_qt5agg import FigureCanvasQTAgg as FigureCanvas
from PyQt5.QtWidgets import QApplication, QVBoxLayout, QHBoxLayout, QMainWindow, QWidget, QPushButton, QStackedWidget
from PyQt5.QtCore import QThread, pyqtSignal
import socket
import json
from PyQt5.QtCore import QTimer


# 创建一个 Matplotlib 画布类
class MplCanvas(FigureCanvas):

    def __init__(self, parent=None, width=5, height=4, dpi=100):
        fig, self.ax = plt.subplots(figsize=(width, height), dpi=dpi)
        super(MplCanvas, self).__init__(fig)


# 服务器线程，负责接收客户端请求
class ServerThread(QThread):
    data_received = pyqtSignal(dict)  # 自定义信号，用于传递接收到的数据

    def __init__(self):
        super(ServerThread, self).__init__()

    def run(self):
        server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        server_socket.bind(('127.0.0.1', 8080))
        server_socket.listen(5)
        print("服务器启动，等待客户端连接...")

        client_socket, addr = server_socket.accept()
        print(f"客户端已连接: {addr}")

        while True:
            data = client_socket.recv(10240)
            #print(f"shuju{data}")
            if not data:
                break

            # 将接收到的 JSON 数据解码
            json_data = json.loads(data.decode('utf-8'))
            print(f"从客户端接收到的数据: {json_data['BuildingEc']}")

            # 发射信号，将数据传递到主线程
            self.data_received.emit(json_data['BuildingEc'])

            # 给客户端发送响应
            response_data = {'message': 'Data received', 'status': 'ok'}
            client_socket.sendall(json.dumps(response_data).encode('utf-8'))


# 创建主窗口
class MainWindow(QMainWindow):

    def __init__(self, *args, **kwargs):
        super(MainWindow, self).__init__(*args, **kwargs)

        # 主控件
        self.main_widget = QWidget(self)
        self.setCentralWidget(self.main_widget)

        # 主布局
        layout = QVBoxLayout(self.main_widget)

        # 创建翻页控件 QStackedWidget
        self.stacked_widget = QStackedWidget()
        layout.addWidget(self.stacked_widget)

        # 添加图表页
        self.create_charts()

        # 添加按钮布局
        btn_layout = QHBoxLayout()
        self.prev_button = QPushButton("上一页")
        self.next_button = QPushButton("下一页")
        btn_layout.addWidget(self.prev_button)
        btn_layout.addWidget(self.next_button)
        layout.addLayout(btn_layout)

        # 绑定按钮事件
        self.prev_button.clicked.connect(self.prev_page)
        self.next_button.clicked.connect(self.next_page)

        # 设置窗口标题
        self.setWindowTitle("PyQt5 with Multiple Charts and Page Switching")

        # 启动服务器线程
        self.server_thread = ServerThread()
        self.server_thread.data_received.connect(self.update_chart)  # 连接信号到槽函数
        self.server_thread.start()

    # 创建多个图表页
    def create_charts(self):
        t = np.linspace(0, 10, 10)

        # 第一页：正弦波
        self.page1 = QWidget()
        page1_layout = QVBoxLayout(self.page1)
        self.sc1 = MplCanvas(self, width=5, height=4, dpi=100)
        self.line1, = self.sc1.ax.plot(t, np.sin(t), label="Sin")
        self.sc1.ax.legend()
        page1_layout.addWidget(self.sc1)
        self.stacked_widget.addWidget(self.page1)

        # 第二页：余弦波
        self.page2 = QWidget()
        page2_layout = QVBoxLayout(self.page2)
        self.sc2 = MplCanvas(self, width=5, height=4, dpi=100)
        self.line2, = self.sc2.ax.plot(t, np.cos(t), label="Cos", color='r')
        self.sc2.ax.legend()
        page2_layout.addWidget(self.sc2)
        self.stacked_widget.addWidget(self.page2)

    # 切换到上一页
    def prev_page(self):
        current_index = self.stacked_widget.currentIndex()
        if current_index > 0:
            self.stacked_widget.setCurrentIndex(current_index - 1)

    # 切换到下一页
    def next_page(self):
        current_index = self.stacked_widget.currentIndex()
        if current_index < self.stacked_widget.count() - 1:
            self.stacked_widget.setCurrentIndex(current_index + 1)

    # 更新图表数据
    def update_chart(self, data):
        print("更新图表数据", data)
        val = data['ret']['moneyHis']
        rates=[item['cost'] for item in val]
        self.line1.set_ydata(rates)
        self.sc1.ax.relim()
        self.sc1.ax.autoscale_view()
        self.sc1.draw()
# 运行应用程序
if __name__ == '__main__':
    app = QApplication(sys.argv)
    main = MainWindow()
    main.show()
    sys.exit(app.exec_())
