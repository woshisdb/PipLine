from sqlalchemy import Column, Integer, Float, ForeignKey, Enum, create_engine, String
from sqlalchemy.orm import relationship, sessionmaker
from sqlalchemy.ext.declarative import declarative_base
from enum import Enum as PyEnum

Base = declarative_base()

class GoodsHistory(Base):
    __tablename__ = 'goods_histories'
    id = Column(Integer, primary_key=True)
    goods_enum = Column(String)
    buy_sum = Column(Integer)  # 购买的数量
    buy_cost = Column(Float)  # 买入的平均价格
    sell_sum = Column(Integer)  # 卖出的数量
    sell_cost = Column(Float)  # 卖出的平均价格
    scene_item_id=Column(Integer, ForeignKey('scene_items.id'))
    scene_item=relationship("SceneItem",uselist=False, back_populates="goods_histories")
    building_item_id = Column(Integer, ForeignKey('building_items.id'))
    building_item = relationship("BuildingItem", uselist=False, back_populates="goods_histories")

    def get_displayable_items(self):
        """
        返回可视化类能展示的项目字典，键为展示数据的名字，值为对应的访问器。
        """
        displayable_items = {
            "buy_sum":self.buy_sum,
            "buy_cost":self.buy_cost,
            "sell_sum":self.sell_sum,
            "sell_cost":self.sell_cost,
        }
        return  displayable_items


class JobHistory(Base):
    __tablename__ = 'job_histories'  # 数据库表名

    id = Column(Integer, primary_key=True)  # 确保主键存在
    building_item_id=Column(Integer, ForeignKey('building_items.id'))  # 外键关联
    building_item=relationship("BuildingItem",uselist=False,back_populates="job_histories")
    job_sum = Column(Integer, comment="工作总数")
    job_cost = Column(Integer, comment="工作成本")
    job = Column(String)

    def get_displayable_items(self):
        """
        返回可视化类能展示的项目字典，键为展示数据的名字，值为对应的访问器。
        """
        displayable_items = {
            "job_sum":self.job_sum,
            "job_cost":self.job_cost,
        }
        return  displayable_items

class PipLineHistory(Base):
    __tablename__ = 'pipline_histories'
    id = Column(Integer, primary_key=True)  # 确保主键存在
    # 其他字段
    goods_create = Column(Integer, comment="创建的产品的数量")
    order_sum = Column(Integer, comment="下令生产的数目")
    carry_sum = Column(Integer, comment="运输数量")
    all_goods = Column(Integer, comment="所有的商品数目")
    building_item_id = Column(Integer, ForeignKey('building_items.id'))  # 外键关联
    building_item=relationship("BuildingItem",uselist=False, back_populates="pipline_history")

    def get_displayable_items(self):
        displayable_items = {
        }
        displayable_items["goods_create"]=self.goods_create
        displayable_items["order_sum"] = self.order_sum
        displayable_items["carry_sum"] = self.carry_sum
        displayable_items["all_goods"] = self.all_goods
        return  displayable_items


class BuildingItem(Base):
    __tablename__ = 'building_items'
    id = Column(Integer, primary_key=True)
    Day = Column(Integer)
    building_obj_id = Column(Integer, ForeignKey('building_objs.id'))  # 外键关联
    building_obj = relationship("BuildingObj",uselist=False, back_populates="building_items")
    pipline_history = relationship("PipLineHistory",uselist=False, back_populates="building_item")  # 反向关系
    # goods_history_id = Column(Integer, ForeignKey('goods_histories.id'))  # 关联到 GoodsHistory
    goods_histories=relationship("GoodsHistory",back_populates="building_item")  # 反向关系
    earn_history_id=Column(Integer, ForeignKey('earn_histories.id'))  # 外键关联
    earn_history=relationship("EarnHistory",uselist=False)
    job_histories=relationship("JobHistory",back_populates="building_item")

    def get_displayable_items(self):
        print(self.Day)
        """
        返回可视化类能展示的项目字典，键为展示数据的名字，值为对应的访问器。
        """
        displayable_items = {
            "Day":self.Day
        }
        for job in self.job_histories:
            print(job)
            jobDic=job.get_displayable_items()
            print(jobDic)
            for key, value in jobDic.items():
                displayable_items[job.job+"_"+key]=value

        for goods in self.goods_histories:
            goodsDic=goods.get_displayable_items()
            for key,value in goodsDic.items():
                displayable_items[goods.goods_enum+"_"+key]=value

        for key,value in self.pipline_history.get_displayable_items().items():
            displayable_items["pipline"+"_"+key]=value

        for key,value in self.earn_history.get_displayable_items().items():
            displayable_items["earn"+"_"+key]=value

        return displayable_items


class EarnHistory(Base):
    __tablename__ = 'earn_histories'  # 数据库表名

    id = Column(Integer, primary_key=True)  # 确保主键存在
    cost = Column(Integer, comment="成本")
    def get_displayable_items(self):
        displayable_items = {
        }
        displayable_items["cost"]=self.cost
        return  displayable_items

class BuildingObj(Base):
    __tablename__ = 'building_objs'
    id=Column(Integer, primary_key=True)
    building_items = relationship("BuildingItem", back_populates="building_obj")


class SceneItem(Base):
    __tablename__ = 'scene_items'
    id = Column(Integer, primary_key=True)
    Day = Column(Integer)
    scene_obj_id = Column(Integer, ForeignKey('scene_objs.id'))  # 外键关联
    scene_obj = relationship("SceneObj", back_populates="scene_items")
    # goods_price_id = Column(Integer, ForeignKey('goods_histories.id'))  # 关联到 GoodsHistory
    goods_histories = relationship("GoodsHistory",back_populates="scene_item")  # 反向关系
    def get_displayable_items(self):
        displayable_items = {
            "Day":self.Day,
        }
        for goods in self.goods_histories:
            goodsDic=goods.get_displayable_items()
            for key,value in goodsDic.items():
                displayable_items[goods.goods_enum+"_"+key]=value
        return  displayable_items



class SceneObj(Base):
    __tablename__ = 'scene_objs'
    id=Column(Integer, primary_key=True)
    scene_items = relationship("SceneItem", back_populates="scene_obj")






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