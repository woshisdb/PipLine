using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GovenControler : MonoBehaviour,IRegisterEvent
{
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        this.Register<NewStepEvent>((e) =>
        {
            text.text = GameArchitect.get.shuiWuJu.money.money+ "$";
        });
    }
}
