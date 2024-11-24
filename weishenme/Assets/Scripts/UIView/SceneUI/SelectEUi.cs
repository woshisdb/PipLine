using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectEUi : MonoBehaviour
{
    public TextMeshProUGUI text;
    public MetaI metaData;
    public void Select()
    {
        Debug.Log("Select");
        ////ѡ��һ��META
        GameArchitect.Instance.uiManager.nowSelect = metaData;
    }
    public void Bind(MetaI meta)
    {
        this.metaData = meta;
        //meta.RetText();
        text.text = meta.RetText();
    }
}
