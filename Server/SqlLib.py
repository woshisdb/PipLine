from sqlalchemy import Column, Integer, Float, ForeignKey, Enum, create_engine
from sqlalchemy.orm import relationship, sessionmaker
from sqlalchemy.ext.declarative import declarative_base
from enum import Enum as PyEnum

Base = declarative_base()


# 定义枚举类
class GoodsEnum(PyEnum):
    HAND = "手"
    IRON_ORE = "铁矿石"
    IRON = "铁"
    POTATO = "土豆"
    COAL = "煤炭"
    AXE = "斧头"
    POTATO_BLOCK = "土豆块"


class Job(PyEnum):
    WORKER = "工人"
    MANAGER = "经理"


# 历史项的基类
class HistoryItem(Base):
    __tablename__ = 'history_items'

    id = Column(Integer, primary_key=True)
    day = Column(Integer)


class GoodsHistory(HistoryItem):
    __tablename__ = 'goods_history'
    id = Column(Integer, ForeignKey('history_items.id'), primary_key=True)  # 外键
    buy_sum = Column(Integer)  # 购买的数量
    buy_cost = Column(Float)  # 买入的平均价格
    sell_sum = Column(Integer)  # 卖出的数量
    sell_cost = Column(Float)  # 卖出的平均价格


class PipLineHistory(HistoryItem):
    __tablename__ = 'pipeline_history'
    id = Column(Integer, ForeignKey('history_items.id'), primary_key=True)  # 外键
    goods_create = Column(Integer)
    order_sum = Column(Integer)
    carry_sum = Column(Integer)
    all_goods = Column(Integer)


class EarnHistory(HistoryItem):
    __tablename__ = 'earn_history'
    id = Column(Integer, ForeignKey('history_items.id'), primary_key=True)  # 外键
    cost = Column(Integer)


class JobHistory(HistoryItem):
    __tablename__ = 'job_history'
    id = Column(Integer, ForeignKey('history_items.id'), primary_key=True)  # 外键
    job_sum = Column(Integer)
    job_cost = Column(Integer)


class MoneyHistory(HistoryItem):
    __tablename__ = 'money_history'
    id = Column(Integer, ForeignKey('history_items.id'), primary_key=True)  # 外键
    money = Column(Integer)


class BuildingItem(HistoryItem):
    __tablename__ = 'building_items'
    id = Column(Integer, ForeignKey('history_items.id'), primary_key=True)  # 外键
    building_goods_prices_id = Column(Integer, ForeignKey('building_goods_prices.id'))
    pip_line_history_id = Column(Integer, ForeignKey('pipeline_history.id'))
    earn_history_id = Column(Integer, ForeignKey('earn_history.id'))

    building_goods_prices = relationship("BuildingGoodsPrices", backref="building_item",foreign_keys=[building_goods_prices_id])
    pip_line_history = relationship("PipLineHistory", backref="building_item",foreign_keys=[pip_line_history_id])
    earn_history = relationship("EarnHistory", backref="building_item",foreign_keys=[earn_history_id])


class NpcItem(HistoryItem):
    __tablename__ = 'npc_items'
    id = Column(Integer, ForeignKey('history_items.id'), primary_key=True)  # 外键
    money_history_id = Column(Integer, ForeignKey('money_history.id'))
    money_history = relationship("MoneyHistory", backref="npc_item",foreign_keys=[money_history_id])


class SceneItem(HistoryItem):
    __tablename__ = 'scene_items'
    id = Column(Integer, ForeignKey('history_items.id'), primary_key=True)  # 外键
    goods_price_id = Column(Integer, ForeignKey('goods_price.id'))
    goods_price = relationship("GoodsPrice", backref="scene_item",foreign_keys=[goods_price_id])


# 商品价格类（为了支持 SceneItem 的映射）
class GoodsPrice(Base):
    __tablename__ = 'goods_price'
    history_items_id = Column(Integer, ForeignKey('history_items.id'), primary_key=True)  # 外键
    id = Column(Integer, primary_key=True)
    goods_enum = Column(Enum(GoodsEnum))
    history_items = relationship("GoodsHistory", backref="goods_price",foreign_keys=[history_items_id])


# 建筑商品价格类，用于处理 BuildingItem 中的字典关系
class BuildingGoodsPrices(Base):
    __tablename__ = 'building_goods_prices'
    history_items_id = Column(Integer, ForeignKey('history_items.id'), primary_key=True)  # 外键
    id = Column(Integer, primary_key=True)
    goods_enum = Column(Enum(GoodsEnum))
    history_items = relationship("GoodsHistory", backref="building_goods_prices",foreign_keys=[history_items_id])


# 数据库连接
DATABASE_URL = "sqlite:///G:/gitP/Pipline/Assets/Resources/Sql/pipline"  # 修改为你的数据库 URL
engine = create_engine(DATABASE_URL)

# 创建所有表
Base.metadata.create_all(engine)

# 创建会话
Session = sessionmaker(bind=engine)
session = Session()

# 关闭会话
session.close()