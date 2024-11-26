import sys
from PyQt5.QtWidgets import (
    QApplication, QMainWindow, QGraphicsScene, QGraphicsView,
    QGraphicsRectItem, QGraphicsTextItem, QGraphicsLineItem,
    QVBoxLayout, QPushButton, QWidget, QGraphicsEllipseItem
)
from PyQt5.QtGui import QPainter, QPen, QBrush
from PyQt5.QtCore import Qt, QPointF, QRectF


class Port(QGraphicsEllipseItem):
    """端口类"""
    def __init__(self, x, y, radius, port_type, node):
        super().__init__(-radius, -radius, 2 * radius, 2 * radius)
        self.setBrush(QBrush(Qt.green if port_type == 'input' else Qt.red))
        self.setPen(QPen(Qt.black))
        self.setFlag(QGraphicsEllipseItem.ItemIsMovable)
        self.setFlag(QGraphicsEllipseItem.ItemSendsGeometryChanges)
        self.setPos(x, y)
        self.radius = radius
        self.port_type = port_type  # 'input' 或 'output'
        self.node = node
        self.connection = None  # 连接到的端口

    def mousePressEvent(self, event):
        if event.button() == Qt.LeftButton:
            self.node.scene.start_connection(self)
        elif event.button() == Qt.RightButton:
            self.node.scene.remove_port(self)
        super().mousePressEvent(event)


class Connection(QGraphicsLineItem):
    """连接类"""
    def __init__(self, start_port, end_port=None):
        super().__init__()
        self.start_port = start_port
        self.end_port = end_port
        self.setPen(QPen(Qt.black, 2))

    def update_position(self):
        start_pos = self.start_port.scenePos()
        if self.end_port:
            end_pos = self.end_port.scenePos()
        else:
            end_pos = self.start_port.node.scene.cursor_pos
        self.setLine(start_pos.x(), start_pos.y(), end_pos.x(), end_pos.y())


class Node(QGraphicsRectItem):
    """节点类"""
    def __init__(self, x, y, label, num_ports, scene):
        super().__init__(0, 0, 100, 50)  # 长方形节点，宽100，高50
        self.setFlag(QGraphicsRectItem.ItemIsMovable)
        self.setFlag(QGraphicsRectItem.ItemSendsGeometryChanges)
        self.setBrush(QBrush(Qt.lightGray))  # 节点背景色
        self.setPen(QPen(Qt.black))  # 节点边框颜色
        self.setPos(x, y)
        self.scene = scene
        self.label = QGraphicsTextItem(label, self)
        self.label.setPos(10, 10)
        self.ports = []  # 存储端口
        self.connections = []  # 存储连接
        self.create_ports(num_ports)

    def create_ports(self, num_ports):
        """创建端口并放置在节点的边缘"""
        rect = self.rect()
        for i in range(num_ports):
            port_type = 'input' if i % 2 == 0 else 'output'
            # 根据端口类型放置在不同的位置
            if port_type == 'input':
                port_x = rect.left()  # 左侧输入端口
            else:
                port_x = rect.right()  # 右侧输出端口
            port_y = rect.top() + i * (rect.height() / num_ports)  # 垂直分布端口
            port = Port(port_x, port_y, 10, port_type, self)
            self.ports.append(port)
            self.scene.addItem(port)

    def mouseMoveEvent(self, event):
        """节点移动时更新端口位置"""
        super().mouseMoveEvent(event)
        # 更新端口位置
        rect = self.rect()
        for i, port in enumerate(self.ports):
            if port.port_type == 'input':
                port.setPos(rect.left(), rect.top() + i * (rect.height() / len(self.ports)))
            else:
                port.setPos(rect.right(), rect.top() + i * (rect.height() / len(self.ports)))

    def mousePressEvent(self, event):
        if event.button() == Qt.LeftButton:
            self.scene.start_connection(self.ports[0])
        super().mousePressEvent(event)


class NodeScene(QGraphicsScene):
    """节点编辑场景"""
    def __init__(self):
        super().__init__()
        self.nodes = []
        self.connections = []
        self.current_connection = None
        self.cursor_pos = QPointF(0, 0)

    def add_node(self, x, y, label="Node", num_ports=3):
        """添加节点"""
        node = Node(x, y, label, num_ports, self)
        self.addItem(node)
        self.nodes.append(node)

    def start_connection(self, start_port):
        """开始连接"""
        self.current_connection = Connection(start_port)
        self.addItem(self.current_connection)

    def finish_connection(self, end_port):
        """完成连接"""
        if self.current_connection:
            self.current_connection.end_port = end_port
            self.connections.append(self.current_connection)
            self.current_connection.update_position()
            self.current_connection = None

    def remove_port(self, port):
        """删除端口"""
        self.removeItem(port)

    def mouseMoveEvent(self, event):
        """鼠标移动事件"""
        self.cursor_pos = event.scenePos()
        if self.current_connection:
            self.current_connection.update_position()
        super().mouseMoveEvent(event)

    def mouseReleaseEvent(self, event):
        """鼠标释放事件"""
        if self.current_connection:
            items = self.items(event.scenePos())
            for item in items:
                if isinstance(item, Port) and item != self.current_connection.start_port:
                    self.finish_connection(item)
                    break
            else:
                self.removeItem(self.current_connection)
            self.current_connection = None
        super().mouseReleaseEvent(event)


class NodeEditor(QMainWindow):
    """主窗口"""
    def __init__(self):
        super().__init__()
        self.setWindowTitle("节点编辑器")
        self.setGeometry(100, 100, 800, 600)

        self.scene = NodeScene()
        self.view = QGraphicsView(self.scene)
        self.view.setRenderHint(QPainter.Antialiasing)

        # 添加 UI 按钮
        self.init_ui()

    def init_ui(self):
        layout = QVBoxLayout()
        add_node_button = QPushButton("添加节点")
        add_node_button.clicked.connect(self.add_node)

        layout.addWidget(self.view)
        layout.addWidget(add_node_button)

        container = QWidget()
        container.setLayout(layout)
        self.setCentralWidget(container)

    def add_node(self):
        """添加新节点"""
        self.scene.add_node(0, 0, f"Node {len(self.scene.nodes) + 1}")


if __name__ == "__main__":
    app = QApplication(sys.argv)
    editor = NodeEditor()
    editor.show()
    sys.exit(app.exec())
