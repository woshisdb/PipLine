public enum GoodsEnum
{
    手 = 1,
    带铁矿石 = 2,
    铁矿石 = 3,
    铁 = 4,
    树木 = 5,
    木材 = 6,
    小麦 = 7,
    土豆 = 8,
    鱼 = 9,
    面粉 = 10,
    煤炭 = 11,
    煤矿 = 12,
    钢材 = 13
}
public class GoodsGen{
public static GoodsInf GetGoodsInf(GoodsEnum goodsEnum){
        if (((int)goodsEnum) < GameArchitect.get.objAsset.goodsInfs.Count)
            return GameArchitect.get.objAsset.goodsInfs[((int)goodsEnum) - 1];
        else
            return null;
        }     public static GoodsObj GetGoodsObj(GoodsEnum goodsEnum){
if (goodsEnum == GoodsEnum.手) { var x = GetGoodsInf(goodsEnum); var y = new HandObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.带铁矿石) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.铁矿石) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.铁) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.树木) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.木材) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.小麦) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.土豆) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.鱼) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.面粉) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.煤炭) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.煤矿) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.钢材) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
return null;}
}
