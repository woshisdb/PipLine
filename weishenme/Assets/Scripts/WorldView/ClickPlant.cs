using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickPlant : MonoBehaviour, IPointerClickHandler
{
    public SceneView SceneView;
    public void OnPointerClick(PointerEventData eventData)
    {
        var wp = eventData.pointerCurrentRaycast.worldPosition;
        Vector2Int pos = new Vector2Int(Mathf.Max( ((int)wp.x)-SceneView.x*10,0 ), Mathf.Max(((int)wp.z)-SceneView.y*10,0)); 
        Debug.Log(pos);
        if(GameArchitect.Instance.uiManager.nowSelect!=null)
        {
            var nowSel = GameArchitect.Instance.uiManager.nowSelect;
            if (nowSel is BuildingMeta)//如果是建筑Meta
            {
                var enu=((BuildingMeta)nowSel).ReturnEnum();
                SceneView.AddBuilding(pos.x,pos.y,enu);
            }
            else if (nowSel is PathMeta)
            {
                var enu=((PathMeta)nowSel).ReturnEnum();
                SceneView.AddPath(pos.x, pos.y, enu);
            }
        }
    }
}
