using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronMiningObj : BuildingObj
{
    public IronMiningObj():base()
    { 
        name = "铁矿厂";
        var obj=GoodsGen.GetGoodsObj(GoodsEnum.带铁矿石);
        obj.sum = 10000000;
        resource.Add(obj);
        this.jobManager = new JobManager(this);
        this.jobManager.jobs.Add(
            typeof(CaiKuangJob),new CaiKuangJob(this)
        );
        this.jobManager.jobs.Add(
            typeof(CarryJob),new CarryJob(this)
        );
        //var t = GameArchitect.get.objAsset.FindTrans("开采铁矿石");
        ////var v = GameArchitect.get.objAsset.FindTrans("搬运商品");
        //var v = new CarryTrans();
        //v.title = "搬运商品";
        //v.maxTrans = 2;
        //v.wasterTimes = 1;
        //v.from.source.Add(new Pair<GoodsEnum, int>(GoodsEnum.带铁矿石, 1));
        //v.to.source.Add(new Pair<GoodsEnum, int>(GoodsEnum.铁矿石, 1));
        //this.pipLineManager.SetTrans(
        //new List<TransNode>()
        //{
        //    new TransNode(t,resource,goodsRes),
        //    new TransNode(v,resource,goodsRes)
        //});


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
