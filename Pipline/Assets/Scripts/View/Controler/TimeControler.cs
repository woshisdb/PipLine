using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct EndStepEvent:IEvent
{
}

public class TimeControler : MonoBehaviour,IRegisterEvent
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = GameArchitect.get.timeSystem.time.ToString();
        this.Register<NewStepEvent>((e) => { text.text = GameArchitect.get.timeSystem.GetTimeStr(); });
    }
}
