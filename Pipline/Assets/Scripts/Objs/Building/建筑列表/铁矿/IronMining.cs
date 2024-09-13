using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IronMiningObj : BuildingObj
{
    public IronMiningObj():base()
    {
        name = "铁矿厂";
        var obj=GoodsGen.GetGoodsObj(GoodsEnum.带铁矿石, 10000000);
        resource.Add(obj);
        InitJob(new CaiKuangJob(this));
        InitTrans("开采铁矿石");
    }
    public override IEnumerator Update()
    {
        yield return base.Update();
    }
}



//public class MovePipLine:Source
//{
//    public MovePipLine(BuildingObj building,Resource from, Resource to, Trans trans, Productivity productivity)
//    {
//        this.belong = building;
//        this.from = from;
//        this.to = to;
//        this.trans = trans;
//        this.productivity = productivity;
//    }

//    public override void Update()
//    {
//        ///来源
//        var fromRes = from.GetGoods<GoodsObj>(trans.from.source[0].Item1);
//        var maxNum = 99999999;
//        foreach(var tran in trans.edge.tras)
//        {
//            var remain=productivity.remain[tran.Key] / tran.Value;
//            maxNum=Mathf.Min(maxNum, remain);
//        }
//        int carryNum= fromRes.sum / trans.from.source[0].Item2;
//        int realcarry=Mathf.Min( Mathf.Min(carryNum, maxNum) , ((MoveTrans)trans).maxTrans);
//        fromRes.sum -= realcarry * trans.from.source[0].Item2;
//        var goods=GoodsGen.GetGoodsObj(trans.from.source[0].Item1);
//        goods.sum += realcarry * trans.from.source[0].Item2;
//        belong.scene.paths[to.building.scene].PushOrder(from,to, goods,trans.wasterTimes);
//    }
//}
