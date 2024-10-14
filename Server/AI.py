import gym
from gym import spaces
import numpy as np
import random
from stable_baselines3 import PPO

# 模拟消费者需求的类
class People:
    def __init__(self, demand_sensitivity=1.0):
        self.demand_sensitivity = demand_sensitivity

    def calculate_demand(self, price, time):
        # 计算消费者对当前价格的需求量，考虑季节性因素
        base_demand = 100
        seasonal_factor = np.sin(time / 365 * 2 * np.pi) + 1  # 季节性影响
        price_sensitivity = np.exp(-self.demand_sensitivity * price)  # 对价格的敏感度
        return base_demand * price_sensitivity * seasonal_factor

# 模拟市场价格波动的类
class Market:
    def __init__(self, num_materials=3):
        self.num_materials = num_materials
        self.material_prices = [100.0 for _ in range(self.num_materials)]  # 原材料初始价格

    def update_material_prices(self, time):
        # 更新原材料价格，考虑季节性和随机波动
        for i in range(self.num_materials):
            base_price = 100
            seasonal_factor = np.sin(time / 365 * 2 * np.pi) + 1  # 季节性影响
            random_fluctuation = random.uniform(-10, 10)  # 随机波动
            self.material_prices[i] = base_price * seasonal_factor + random_fluctuation

# 工厂的强化学习环境类
class FactoryEnv(gym.Env):
    def __init__(self):
        super(FactoryEnv, self).__init__()

        # 定义动作空间：连续的价格和生产量调整
        self.action_space = spaces.Box(low=np.array([-10.0, -20.0]), high=np.array([10.0, 20.0]), dtype=np.float32)

        # 定义状态空间：产品价格和生产量
        self.observation_space = spaces.Box(low=np.array([50.0, 50.0]), high=np.array([200.0, 200.0]), dtype=np.float32)

        # 工厂参数
        self.price = 100.0  # 产品初始价格
        self.production_level = 100.0  # 初始生产水平
        self.time = 0  # 初始时间

        # 初始化消费者和市场
        self.people = People(demand_sensitivity=1.0)
        self.market = Market(num_materials=3)

    def reset(self):
        # 重置环境状态
        self.price = 100.0
        self.production_level = 100.0
        self.time = 0
        self.market = Market(num_materials=3)  # 重置市场
        return np.array([self.price, self.production_level], dtype=np.float32)

    def step(self, action):
        # 解析动作
        price_adjustment, production_adjustment = action

        # 更新价格和生产水平
        self.price = np.clip(self.price + price_adjustment, 50.0, 200.0)
        self.production_level = np.clip(self.production_level + production_adjustment, 50.0, 200.0)

        # 更新时间
        self.time += 1

        # 更新原材料价格
        self.market.update_material_prices(self.time)

        # 计算需求和利润
        demand = self.people.calculate_demand(self.price, self.time)
        cost = sum(self.market.material_prices[i] * 10 for i in range(self.market.num_materials))  # 假设每种原材料需要 10 单位
        revenue = self.price * min(demand, self.production_level)
        profit = revenue - cost

        # 计算奖励 (使用利润作为奖励)
        reward = profit

        # 检查是否结束 (例如达到一定时间步数)
        done = self.time >= 365

        # 构建状态
        state = np.array([self.price, self.production_level], dtype=np.float32)

        return state, reward, done, {}

    def render(self, mode='human'):
        # 打印当前状态（可选）
        print(f"Time: {self.time}, Price: {self.price}, Production: {self.production_level}, Material Prices: {self.market.material_prices}")

    def close(self):
        pass

# 使用 Stable Baselines3 训练模型
def train_factory_model():
    # 创建环境
    env = FactoryEnv()

    # 使用 PPO 算法进行训练
    model = PPO('MlpPolicy', env, verbose=1)
    model.learn(total_timesteps=10000)

    # 保存模型
    model.save("factory_ppo")

    # 测试模型
    obs = env.reset()
    for _ in range(1000):
        action, _states = model.predict(obs)
        obs, rewards, done, info = env.step(action)
        env.render()
        if done:
            break

# 运行训练过程
if __name__ == "__main__":
    train_factory_model()
