from flask import Flask, jsonify, request
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker

from SqlLib import GoodsHistory, BuildingItem, PipLineHistory, EarnHistory, JobHistory, SceneItem, GoodsPrice, \
    GoodsEnum, NpcItem, MoneyHistory

app = Flask(__name__)

# 数据库连接
DATABASE_URL = "sqlite:///G:/gitP/Pipline/Assets/Resources/Sql/pipline"  # 修改为你的数据库 URL
engine = create_engine(DATABASE_URL)

# 创建会话
Session = sessionmaker(bind=engine)

@app.route('/scene', methods=['POST'])
def receive_scene():
    print("data")
    # 从请求中获取 JSON 数据
    data = request.json['ec']
    print("data",data)  # 打印接收到的数据用于调试

    # 创建数据库会话
    session = Session()
    print(1)
    print("data", data['Day'])
    # 创建 SceneItem 实例
    scene_item = SceneItem(id=request.json['id'],day=data['Day'])
    print(2)
    # 处理商品价格
    goods_prices = data.get('goods_price', {})
    for goods_enum, goods_history_data in goods_prices.items():
        goods_price = GoodsPrice(goods_enum=GoodsEnum[goods_enum])
        goods_history = GoodsHistory(
            buy_sum=goods_history_data.get('buy_sum', 0),
            buy_cost=goods_history_data.get('buy_cost', 0.0),
            sell_sum=goods_history_data.get('sell_sum', 0),
            sell_cost=goods_history_data.get('sell_cost', 0.0)
        )
        goods_price.history_items.append(goods_history)
        scene_item.goods_price.append(goods_price)

    # 将 SceneItem 添加到会话
    session.add(scene_item)

    # 提交事务
    session.commit()
    session.close()  # 关闭会话
    return "Success", 201

@app.route('/npc', methods=['POST'])
def receive_npc():
    # 从请求中获取 JSON 数据
    data = request.json

    # 创建数据库会话
    session = Session()
    try:
        # 创建 NpcItem 实例
        npc_item = NpcItem(day=data['day'])

        # 处理金钱历史数据
        money_history_data = data.get('money_history', {})
        money_history = MoneyHistory(money=money_history_data.get('money', 0))
        npc_item.money_history = money_history
        print(npc_item)
        # 将 NpcItem 添加到会话
        session.add(npc_item)

        # 提交事务
        session.commit()
        return "Success", 201
    except Exception as e:
        session.rollback()  # 回滚事务
        return "Error", 500
    finally:
        session.close()  # 关闭会话

@app.route('/building', methods=['POST'])
def receive_building():
    data = request.json

    # 创建 BuildingItem 实例
    building_item = BuildingItem(day=data['day'])

    # 将 GoodsHistory 添加到 BuildingItem
    for item, history in data['building_goods_prices'].items():
        goods_history = GoodsHistory(
            BuySum=history['BuySum'],
            BuyCost=history['BuyCost'],
            SellSum=history['SellSum'],
            SellCost=history['SellCost'],
            day=data['day']  # 保存对应的 day
        )
        building_item.building_goods_prices.append(goods_history)

    # 添加 OutputPipline
    output_pipeline_data = data['output_pipline']
    output_pipeline = PipLineHistory(
        GoodsCreate=output_pipeline_data['GoodsCreate'],
        OrderSum=output_pipeline_data['OrderSum'],
        CarrySum=output_pipeline_data['CarrySum'],
        AllGoods=output_pipeline_data['AllGoods'],
        day=data['day']  # 保存对应的 day
    )
    building_item.output_pipline.append(output_pipeline)
    print("wqwe1")
    # 添加 EarnHistory
    earn_history_data = data['money_his']
    earn_history = EarnHistory(
        Cost=earn_history_data['Cost'],
        day=data['day']  # 保存对应的 day
    )
    building_item.money_his.append(earn_history)

    # 添加 JobHistory（如果有的话）
    for job, job_history in data.get('job_his', {}).items():
        job_history_item = JobHistory(
            JobSum=job_history['JobSum'],
            JobCost=job_history['JobCost'],
            day=data['day']  # 保存对应的 day
        )
        building_item.job_his.append(job_history_item)
    print("wqwe",building_item)
    # 保存到数据库
    session = Session()
    session.add(building_item)
    session.commit()
    session.close()

    return jsonify({"status": "success"}), 201

if __name__ == '__main__':
    # 设置 host 为 127.0.0.1 和 port 为 8080
    app.run(host='127.0.0.1', port=8080, debug=True)
