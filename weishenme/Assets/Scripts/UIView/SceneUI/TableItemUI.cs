using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TableItemUI : UIItem
{
    public TextMeshProUGUI key;
    public ListUI listUI;

    public override void BindObj(UIItemBinder tableItemBinder)
    {
        binder = tableItemBinder;
        key.SetText(binder.getKey());
        var b = (TableItemBinder)binder;
        foreach (var item in b.items)
        {
            if(item.GetType()==typeof(KVItemBinder))
            {
                GameObject go = GameObject.Instantiate(GameArchitect.Instance.uiManager.kvItemUI);
                var comp=go.GetComponent<KVItemUI>();
                comp.BindObj(item);
                listUI.Add(go);
            }
            else
            {
                GameObject go = GameObject.Instantiate(GameArchitect.Instance.uiManager.tableItemUI);
                var comp = go.GetComponent<TableItemUI>();
                comp.BindObj((TableItemBinder)item);
                listUI.Add(go);
            }
        }
    }
}
