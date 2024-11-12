using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct SelectViewEvent:IEvent
{
    public BaseView view;
    public SelectViewEvent(BaseView baseView)
    {
        view = baseView;
    }
}

public class DetailUI : MonoBehaviour,IRegisterEvent
{
    public ScrollRect detail;
    public RectTransform contentTransform;
    public (StringInputItem, TextInputItem[])? detailContent;
    public void Start()
    {
        this.Register<SelectViewEvent>(e =>
        {
            detailContent=e.view.SelectIt();//ѡ�����
            //var item1=detailContent.;
            //var item2=detailContent.Item2;
            //foreach(var item in item2)
            //{
            //    ///��������
            //    if(item is BoolInputItem)
            //    {

            //    }
            //    else if(item is FloatInputItem)
            //    {

            //    }
            //    else if(item is IntInputItem)
            //    {

            //    }
            //}
        });
    }
    public void RefreshScrollRect()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentTransform);
    }
}
