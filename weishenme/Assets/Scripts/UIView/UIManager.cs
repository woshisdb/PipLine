using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class UIManager : MonoBehaviour
{
    public InspectorUI inspector;
    public GameObject kvItemUI;
    public GameObject tableItemUI;
    public GameObject obj;
    public GameObject metaUI;
    public SelectViews selectUI;
    /// <summary>
    /// 正在选择的Meta
    /// </summary>
    [ShowInInspector]
    public MetaI nowSelect;

    public TextMeshProUGUI selectText;

    public void Update()
    {
        if(nowSelect!=null)
        {
            if(nowSelect is BuildingMeta)
            {
                selectText.text = ((BuildingMeta)nowSelect).ReturnEnum().ToString();
            }
            else if(nowSelect is PathMeta)
            {
                selectText.text= ((PathMeta)nowSelect).ReturnEnum().ToString();
            }
        }
    }
}
