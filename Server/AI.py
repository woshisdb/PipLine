import numpy as np
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestRegressor
from econml.dml import LinearDML
import matplotlib.pyplot as plt

# Step 1: 模拟数据
np.random.seed(42)

# X 表示特征变量（如人口统计信息）
n_samples = 1000
X = np.random.normal(0, 1, size=(n_samples, 3))

# T 表示干预变量（如广告支出）
T = np.random.normal(0, 1, size=n_samples)

# Y 表示结果变量（如销售收入），假设有一个因果影响 T 对 Y 的影响
# Y = 2*T + 0.5*X[:, 0] + 0.3*X[:, 1] + noise
noise = np.random.normal(0, 0.1, size=n_samples)
Y = 2 * T + 0.5 * X[:, 0] + 0.3 * X[:, 1] + noise

# Step 2: 数据集划分
X_train, X_test, T_train, T_test, Y_train, Y_test = train_test_split(X, T, Y, test_size=0.2, random_state=42)

# Step 3: 建立模型
est = LinearDML(
    model_y=RandomForestRegressor(),  # Y 的回归模型
    model_t=RandomForestRegressor(),  # T 的回归模型
    random_state=42
)

# Step 4: 训练模型
est.fit(Y_train, T_train, X=X_train)

# Step 5: 估计因果影响
treatment_effect = est.effect(X_test)

# 打印平均处理效应
print("Estimated Average Treatment Effect (ATE):", treatment_effect.mean())

# Step 6: 可视化
plt.hist(treatment_effect, bins=30, alpha=0.7)
plt.xlabel("Treatment Effect")
plt.ylabel("Frequency")
plt.title("Distribution of Estimated Treatment Effects")
plt.show()
