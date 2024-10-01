import sys
import tkinter as tk
from tkinter import simpledialog, messagebox
from tkinter import ttk  # 从 ttk 导入进度条和其他控件
import matplotlib.pyplot as plt
import pandas as pd
from PyQt5.QtWidgets import QApplication, QMainWindow, QVBoxLayout, QCheckBox, QPushButton, QWidget
from sqlalchemy import create_engine, inspect
from sqlalchemy.orm import sessionmaker

from SqlLib import SceneItem, BuildingItem
plt.rcParams['font.sans-serif'] = ['SimHei']  # 设置字体为 SimHei
plt.rcParams['axes.unicode_minus'] = False  # 解决负号显示问题

class SQLAlchemyVisualizer:
    def __init__(self, model_class, session):
        self.model_class = model_class
        self.session = session

    def visualize(self):
        # 查询数据
        data = self.session.query(self.model_class).all()
        first_item = data[0] if data else None
        if first_item is not None:
            # 将数据组织成列表，列出所有元素的展示值
            displayable_items = first_item.get_displayable_items()
            structured_data = []
            for item in data:
                row = {key: getattr(item, value.__name__)() if callable(value) else value for key, value in
                       item.get_displayable_items().items()}
                structured_data.append(row)

            # 将数据转换为 DataFrame
            df = pd.DataFrame(structured_data)

            # 按照 'day' 字段排序（如果存在）
            if 'Day' in df.columns:
                df = df.sort_values(by='Day')

            # 使用列选择功能
            selected_columns = self.select_columns(df.columns.tolist())
            df = df[selected_columns]
            print(df)
            # 可视化
            df.plot(kind='bar', x='Day' if 'Day' in df.columns else df.columns[0])  # 使用 'Day' 作为 x 轴
            plt.title(f"Visualization of {self.model_class.__name__} by Day")
            plt.xlabel('Day' if 'Day' in df.columns else 'Index')
            plt.ylabel('Values')
            plt.show()

    def select_columns(self, columns):
        # 创建 tkinter 窗口
        root = tk.Tk()
        root.title("选择列")

        selected_columns = []
        var_list = []  # 存储复选框变量

        def on_ok():
            # 获取选中的列
            selected_columns.clear()  # 清空已选列表
            for i, var in enumerate(var_list):
                if var.get():
                    selected_columns.append(columns[i])
            root.quit()  # 结束主循环，关闭窗口

        # 创建主框架
        main_frame = tk.Frame(root)
        main_frame.grid(row=0, column=0, sticky="nsew", padx=10, pady=10)

        # 使主框架可扩展
        root.grid_rowconfigure(0, weight=1)
        root.grid_columnconfigure(0, weight=1)

        # 创建滚动框架
        canvas = tk.Canvas(main_frame)
        scrollbar = tk.Scrollbar(main_frame, orient="vertical", command=canvas.yview)
        scrollable_frame = tk.Frame(canvas)

        scrollable_frame.bind(
            "<Configure>",
            lambda e: canvas.configure(scrollregion=canvas.bbox("all"))
        )

        canvas.create_window((0, 0), window=scrollable_frame, anchor="nw")
        canvas.configure(yscrollcommand=scrollbar.set)

        # 在滚动框架中添加复选框
        for column in columns:
            var = tk.BooleanVar()  # 创建布尔变量用于复选框
            var_list.append(var)  # 添加到变量列表
            checkbox = tk.Checkbutton(scrollable_frame, text=column, variable=var)
            checkbox.pack(anchor='w')  # 复选框在左对齐

        # Pack the canvas and scrollbar
        canvas.grid(row=0, column=0, sticky="nsew")
        scrollbar.grid(row=0, column=1, sticky="ns")

        # 使 canvas 和 scrollable_frame 可扩展
        main_frame.grid_rowconfigure(0, weight=1)
        main_frame.grid_columnconfigure(0, weight=1)

        # 绑定鼠标滚轮事件
        def scroll(event):
            canvas.yview_scroll(int(-1 * (event.delta / 120)), "units")  # 向上或向下滚动
            return "break"  # 防止事件传播

        # 绑定鼠标滚轮事件到 canvas
        canvas.bind_all("<MouseWheel>", scroll)

        # Add a button to confirm the selection
        ok_button = tk.Button(root, text="确定", command=on_ok)
        ok_button.grid(row=1, column=0, pady=(5, 10), sticky="ew")

        # 进度条
        progress = tk.DoubleVar()
        progress_bar = ttk.Progressbar(root, variable=progress, maximum=100)  # 从 ttk 导入进度条
        progress_bar.grid(row=2, column=0, pady=(0, 10), sticky="ew")

        # 使按钮和进度条可扩展
        root.grid_rowconfigure(1, weight=0)
        root.grid_rowconfigure(2, weight=0)

        # 模拟进度条更新（可选）
        def update_progress():
            for i in range(100):
                progress.set(i + 1)
                root.update_idletasks()
                root.after(20)  # 模拟延时更新进度条
            progress.set(100)

        root.after(0, update_progress)  # 开始进度条更新

        root.mainloop()  # 进入主循环

        # 如果没有选中列，给出警告
        if not selected_columns:
            messagebox.showwarning("警告", "未选择任何列！")
            return columns.tolist()  # 默认返回所有列

        return selected_columns


DATABASE_URL = "sqlite:///G:/gitP/Pipline/Assets/Resources/Sql/pipline"  # 修改为你的数据库 URL
engine = create_engine(DATABASE_URL)
# Usage example
# Create session
Session = sessionmaker(bind=engine)
session = Session()

# Assume you have a SQLAlchemy model class `SceneItem`
visualizer = SQLAlchemyVisualizer(BuildingItem, session)
visualizer.visualize()
# Run the PyQt5 application


