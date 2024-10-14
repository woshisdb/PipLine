import torch
import torch.nn as nn
import torch.optim as optim
import numpy as np
from sklearn.linear_model import LinearRegression

# 决策生成器 (Decision Generator) 使用PyTorch
class DecisionGenerator(nn.Module):
    def __init__(self, input_size, output_size):
        super(DecisionGenerator, self).__init__()
        # 构建深度学习模型，使用两层神经网络
        self.hidden_layer = nn.Linear(input_size, 10)  # 隐藏层，10个节点
        self.output_layer = nn.Linear(10, output_size)  # 输出层，决策维度

    def forward(self, x):
        # 前向传播
        x = torch.sigmoid(self.hidden_layer(x))  # 使用sigmoid激活函数
        x = self.output_layer(x)  # 线性输出层
        return x

    def train_model(self, inputs, outputs, epochs=1000, learning_rate=0.01):
        # 定义损失函数和优化器
        criterion = nn.MSELoss()
        optimizer = optim.Adam(self.parameters(), lr=learning_rate)

        # 将numpy数据转换为tensor
        inputs = torch.tensor(inputs, dtype=torch.float32)
        outputs = torch.tensor(outputs, dtype=torch.float32)

        for epoch in range(epochs):
            optimizer.zero_grad()  # 清空梯度
            predicted = self.forward(inputs)  # 前向传播
            loss = criterion(predicted, outputs)  # 计算损失
            loss.backward()  # 反向传播
            optimizer.step()  # 优化参数

            if epoch % 100 == 0:
                print(f'Epoch {epoch}, Loss: {loss.item()}')

    def generate_decision(self, current_state):
        # 生成决策，输入特征
        current_state = torch.tensor(current_state, dtype=torch.float32).unsqueeze(0)
        predicted_values = self.forward(current_state)
        return predicted_values.detach().numpy()[0]  # 返回预测值

# 估计器 (Estimator)
class Estimator:
    def __init__(self):
        # 初始化线性回归模型
        self.regression = LinearRegression()

    def train(self, inputs, outputs):
        # 训练回归模型
        self.regression.fit(inputs, outputs)

    def estimate_next_state(self, current_state, decision_features):
        # 将当前状态与决策组合为输入
        input_features = np.concatenate([current_state, decision_features]).reshape(1, -1)
        # 预测未来状态
        return self.regression.predict(input_features)[0]

# 选择器 (Selector)
class Selector:
    def __init__(self, production_weight, sales_weight, revenue_weight):
        # 设置不同权重
        self.production_weight = production_weight
        self.sales_weight = sales_weight
        self.revenue_weight = revenue_weight

    def evaluate_future_state(self, future_state):
        # 提取未来状态中的生产、销售和收入
        production = future_state[0]
        sales = future_state[1]
        revenue = future_state[2]
        # 根据权重计算综合评分
        score = (self.production_weight * production +
                 self.sales_weight * sales +
                 self.revenue_weight * revenue)
        return score

