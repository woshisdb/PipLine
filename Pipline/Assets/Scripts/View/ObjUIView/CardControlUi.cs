using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardControlUi : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI content;
    public Button button;
    public CardInf cardInf;
    public UnityAction action;
    public void Init()
    {
        this.title.text = cardInf.title;
        this.content.text = cardInf.description;
    }
    public void SetCardInf(CardInf cardInf)
    {
        this.cardInf = cardInf;
        action = this.cardInf.effect;
        Init();
    }
    public void CallAction()
    {
        action.Invoke();
    }

}
