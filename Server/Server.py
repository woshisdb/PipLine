from flask import Flask, jsonify, request
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker

from SqlLib import GoodsHistory, SceneItem, GoodsHistory, SceneObj, BuildingItem, PipLineHistory, EarnHistory, \
    JobHistory, BuildingObj

app = Flask(__name__)

# 数据库连接
DATABASE_URL = "sqlite:///G:/gitP/Pipline/Assets/Resources/Sql/pipline"  # 修改为你的数据库 URL
engine = create_engine(DATABASE_URL)

# 创建会话
Session = sessionmaker(bind=engine)

@app.route('/scene', methods=['POST'])
def receive_scene():
    print("data",request.json)
    # 从请求中获取 JSON 数据
    data = request.json['ec']
    print("data",data)  # 打印接收到的数据用于调试

    # 创建数据库会话
    session = Session()
    print("data", data['Day'])
    # 创建 SceneItem 实例
    # scenobj=SceneObj(id=request.json['id'])
    scenobj = session.query(SceneObj).filter_by(id=request.json['id']).first()
    if scenobj is None:
        scenobj = SceneObj(id=request.json['id'])
        session.add(scenobj)  # 添加新的 SceneObj 到会话
    # 将 SceneItem 添加到会话
    # session.add(scenobj)
    scene_item = SceneItem(Day=data['Day'],scene_obj=scenobj)
    session.add(scene_item)
    # 处理商品价格
    goods_prices = data.get('goodsPrice', {})
    for goods_enum, goods_history_data in goods_prices.items():
        goods_history = GoodsHistory(
            scene_item=scene_item,
            buy_sum=goods_history_data.get('BuySum', 0),
            buy_cost=goods_history_data.get('BuyCost', 0.0),
            sell_sum=goods_history_data.get('SellSum', 0),
            sell_cost=goods_history_data.get('SellCost', 0.0),
            goods_enum=goods_enum
        )
        # session.add(goods_history)
    # 提交事务
    session.add(scene_item)
    session.commit()
    session.close()  # 关闭会话
    return "Success", 201

# @app.route('/npc', methods=['POST'])
# def receive_npc():
#     # 从请求中获取 JSON 数据
#     data = request.json
#
#     # 创建数据库会话
#     session = Session()
#     try:
#         # 创建 NpcItem 实例
#         npc_item = NpcItem(day=data['day'])
#
#         # 处理金钱历史数据
#         money_history_data = data.get('money_history', {})
#         money_history = MoneyHistory(money=money_history_data.get('money', 0))
#         npc_item.money_history = money_history
#         print(npc_item)
#         # 将 NpcItem 添加到会话
#         session.add(npc_item)
#
#         # 提交事务
#         session.commit()
#         return "Success", 201
#     except Exception as e:
#         session.rollback()  # 回滚事务
#         return "Error", 500
#     finally:
#         session.close()  # 关闭会话
#
@app.route('/building', methods=['POST'])
def receive_building():
    data = request.json['ec']
    print(data)
    # 保存到数据库
    session = Session()
    buildingobj = session.query(BuildingObj).filter_by(id=request.json['id']).first()
    if buildingobj is None:
        buildingobj = BuildingObj(id=request.json['id'])
        session.add(buildingobj)  # 添加新的 SceneObj 到会话
    # 创建 BuildingItem 实例
    building_item = BuildingItem(
        Day=data['Day'],
        building_obj=buildingobj,

    )

    # 将 GoodsHistory 添加到 BuildingItem
    for item, history in data['buildingGoodsPrices'].items():
        goods_history = GoodsHistory(
            goods_enum=item,
            buy_sum=history['BuySum'],
            buy_cost=history['BuyCost'],
            sell_sum=history['SellSum'],
            sell_cost=history['SellCost'],
        )
        building_item.goods_histories.append(goods_history)

    # 添加 OutputPipline
    output_pipeline_data = data['pipLineHistory']
    output_pipeline = PipLineHistory(
        goods_create=output_pipeline_data['GoodsCreate'],
        order_sum=output_pipeline_data['OrderSum'],
        carry_sum=output_pipeline_data['CarraySum'],
        all_goods=output_pipeline_data['AllGoods'],
    )
    building_item.pipline_history=output_pipeline
    # 添加 EarnHistory
    earn_history_data = data['earnHistory']
    earn_history = EarnHistory(
        cost=earn_history_data['Cost'],
    )
    building_item.earn_history=earn_history

    # 添加 JobHistory（如果有的话）
    for job, job_history in data.get('jobHis', {}).items():
        job_history_item = JobHistory(
            job_sum=job_history['JobSum'],
            job_cost=job_history['JobCost'],
            job=job
        )
        building_item.job_histories.append(job_history_item)
    # session.add(buildingobj)
    session.add(building_item)
    session.commit()
    session.close()

    return jsonify({"status": "success"}), 201

if __name__ == '__main__':
    # 设置 host 为 127.0.0.1 和 port 为 8080
    app.run(host='127.0.0.1', port=8080, debug=True)
