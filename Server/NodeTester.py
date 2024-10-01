import tkinter as tk
from tkinter import messagebox

class Node:
    def __init__(self, name, x, y):
        self.name = name
        self.x = x
        self.y = y
        self.connections = []

    def add_connection(self, other_node):
        if other_node not in self.connections:
            self.connections.append(other_node)

class NodeEditor:
    def __init__(self, master):
        self.master = master
        self.master.title("Node Editor")
        self.canvas = tk.Canvas(master, bg="white")
        self.canvas.pack(fill=tk.BOTH, expand=True)

        self.nodes = {}
        self.selected_node = None

        self.canvas.bind("<Button-1>", self.on_click)
        self.canvas.bind("<B1-Motion>", self.on_drag)

    def add_node(self, name, x, y):
        node = Node(name, x, y)
        self.nodes[node] = self.canvas.create_oval(x - 20, y - 20, x + 20, y + 20, fill="lightblue")
        self.canvas.create_text(x, y, text=name)
        return node

    def on_click(self, event):
        for node in self.nodes.keys():
            x, y = node.x, node.y
            if x - 20 < event.x < x + 20 and y - 20 < event.y < y + 20:
                self.selected_node = node
                break

        if self.selected_node:
            self.show_connections()

    def on_drag(self, event):
        if self.selected_node:
            self.canvas.coords(self.nodes[self.selected_node], event.x - 20, event.y - 20, event.x + 20, event.y + 20)
            self.selected_node.x, self.selected_node.y = event.x, event.y
            self.redraw_connections()

    def show_connections(self):
        connections = self.selected_node.connections
        if connections:
            msg = f"{self.selected_node.name} is connected to: " + ", ".join([n.name for n in connections])
        else:
            msg = f"{self.selected_node.name} has no connections."
        messagebox.showinfo("Node Connections", msg)

    def redraw_connections(self):
        self.canvas.delete("line")
        for node in self.nodes.keys():
            for conn in node.connections:
                self.canvas.create_line(node.x, node.y, conn.x, conn.y, fill="black", tags="line")

if __name__ == "__main__":
    root = tk.Tk()
    editor = NodeEditor(root)

    # Adding some example nodes
    node_a = editor.add_node("Node A", 100, 100)
    node_b = editor.add_node("Node B", 200, 200)
    node_c = editor.add_node("Node C", 300, 100)

    # Adding connections
    node_a.add_connection(node_b)
    node_b.add_connection(node_c)

    root.mainloop()
