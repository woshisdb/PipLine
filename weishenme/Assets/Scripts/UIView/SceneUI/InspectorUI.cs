using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InspectorUI : MonoBehaviour
{
    public ListUI listUI;
    public void Start()
    {
        
    }
    public void ShowUI(BaseObj obj)
    {
        listUI.ClearUis();
        var uis=obj.GetUI();
        foreach (var item in uis)
        {
            if (item.GetType() == typeof(KVItemBinder))
            {
                GameObject go = GameObject.Instantiate(GameArchitect.Instance.uiManager.kvItemUI);
                var comp = go.GetComponent<KVItemUI>();
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