public enum GoodsEnum
{
    手 = 1,
    带铁矿石 = 2,
    铁矿石 = 3,
    铁 = 4,
    土豆 = 5,
    煤炭 = 6,
    煤矿 = 7,
    斧头 = 8,
    土豆块 = 9
}
public class GoodsGen{
public static GoodsInf GetGoodsInf(GoodsEnum goodsEnum){
        return GameArchitect.get.objAsset.goodsMap[goodsEnum];
        }     public static GoodsObj GetGoodsObj(GoodsEnum goodsEnum,int sum=0){
if (goodsEnum == GoodsEnum.手) { var x = GetGoodsInf(goodsEnum); var y = new HandObj(); y.sum=sum; y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.带铁矿石) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.sum=sum; y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.铁矿石) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.sum=sum; y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.铁) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.sum=sum; y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.土豆) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.sum=sum; y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.煤炭) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.sum=sum; y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.煤矿) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.sum=sum; y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.斧头) { var x = GetGoodsInf(goodsEnum); var y = new ToolObj(); y.sum=sum; y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.土豆块) { var x = GetGoodsInf(goodsEnum); var y = new FoodObj(); y.sum=sum; y.goodsInf = x; return y; }
return null;}
}
