using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M_bt : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TMPro.TMP_Text mytext;
    public void OnPointerDown(PointerEventData eventData)
    {
        mytext.color = Color.black;
        mytext.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, -5.0f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        mytext.color = Color.white;
        mytext.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, 5.0f);
    }

}
