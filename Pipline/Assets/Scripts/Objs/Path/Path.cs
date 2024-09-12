using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOrder
{
    public void Complete();
    public void Begin();
}

public struct CargoPathOrder:IOrder
{
    public Resource from;
    public Resource to;
    public GoodsObj cargo;
    public int wasterTime;

    public CargoPathOrder(Resource from, Resource to, GoodsObj npc, int wasterTime) : this()
    {
        this.from = from;
        this.to = to;
        this.cargo = npc;
        this.wasterTime = wasterTime;
    }
    public void Complete()
    {
        to.Add(cargo);
    }
    public void Begin()
    {
        from.Remove(cargo);
    }
}
public struct NpcPathOrder : IOrder
{
    public SceneObj from;
    public SceneObj to;
    public NpcObj cargo;
    public int wasterTime;

    public NpcPathOrder(SceneObj from, SceneObj to, NpcObj npc, int wastTime) : this()
    {
        this.from = from;
        this.to = to;
        this.cargo = npc;
        this.wasterTime = wastTime;
    }
    public void Complete()
    {
        to.Enter(cargo);
    }
    public void Begin()
    {
        from.Leave(cargo);
    }
}

public class PathObj : BaseObj
{
    public int maxTime = 100;
    public List<Path> path;
    public CircularQueue<List<IOrder>> orders;
    public SceneObj scene;
    public PathObj(List<Path> path,SceneObj scene) : base()
    {
        this.path = path;
        this.scene = scene;
        orders = new CircularQueue<List<IOrder>>(maxTime);
        for(var i=0;i<maxTime; i++)
        orders.Enqueue(new List<IOrder>());
    }
    //public void PushOrder(SceneObj from,SceneObj to,NpcObj npc)
    //{
    //    var t = new NpcPathOrder(from, to, npc, path.wastTime);
    //    t.Begin();
    //    orders.Find(0).Add(t);
    //}
    /// <summary>
    /// 花费的价格
    /// 时间
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="goods"></param>
    /// <param name="cost"></param>
    /// <param name="wasterTime"></param>
    public void PushOrder(Resource from, Resource to,GoodsObj goods,int wasterTime)
    {
        var t=new CargoPathOrder(from, to, goods, wasterTime);
        t.Begin();
        orders.FindFront(wasterTime-1).Add(t);
    }
    public void Update()
    {
        var last=orders.Find(orders.Size() - 1);
        foreach(var o in last)
        {
            o.Complete();
        }
        orders.Dequeue();
        orders.Enqueue();
        orders.Find(0).Clear();
    }
}
