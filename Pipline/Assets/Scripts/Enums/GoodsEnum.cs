using UnityEngine;

public enum GoodsEnum
{
    手 = 1,
    带铁矿石 = 2,
    铁矿石 = 3,
    铁 = 4
}
public class GoodsGen{
public static GoodsInf GetGoodsInf(GoodsEnum goodsEnum){
        if (((int)goodsEnum) <= GameArchitect.get.objAsset.goodsInfs.Count)
            return GameArchitect.get.objAsset.goodsInfs[((int)goodsEnum) - 1];
        else
            return null;
        }     public static GoodsObj GetGoodsObj(GoodsEnum goodsEnum){
if (goodsEnum == GoodsEnum.手) { var x = GetGoodsInf(goodsEnum); var y = new HandObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.带铁矿石) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.铁矿石) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
if (goodsEnum == GoodsEnum.铁) { var x = GetGoodsInf(goodsEnum); var y = new GoodsObj(); y.goodsInf = x; return y; }
return null;}
}
