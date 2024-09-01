using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaiKuangAct : Act
{
    public override IEnumerator Run()
    {

    }

    public override int WasterTime()
    {

    }
}

public class CaiKuang : Job
{
    public CaiKuang()
    {
        dayWorks = new List<DayWork>();
        var workday=new DayWork();
        workday.preAct=new SeqNpcAct(
            new UseToolAct()
        );
        dayWorks.Add(workday);
    }
}
