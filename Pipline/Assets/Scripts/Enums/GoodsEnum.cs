public enum GoodsEnum
{
    手 = 1,
    带铁矿石 = 2,
    铁矿石 = 3,
    铁 = 4
}
public class GoodsGen
{
    public static GoodsInf GetGoodsInf(GoodsEnum goodsEnum)
    {
        if (goodsEnum == GoodsEnum.手) { return new HandInf(); }
        if (goodsEnum == GoodsEnum.带铁矿石) { return new GoodsInf(); }
        if (goodsEnum == GoodsEnum.铁矿石) { return new GoodsInf(); }
        if (goodsEnum == GoodsEnum.铁) { return new GoodsInf(); }
        return null;
    }
    public static GoodsObj GetGoodsObj(GoodsEnum goodsEnum)
    {
        if (goodsEnum == GoodsEnum.手) { return new HandObj(); }
        if (goodsEnum == GoodsEnum.带铁矿石) { return new GoodsObj(); }
        if (goodsEnum == GoodsEnum.铁矿石) { return new GoodsObj(); }
        if (goodsEnum == GoodsEnum.铁) { return new GoodsObj(); }
        return null;
    }
}