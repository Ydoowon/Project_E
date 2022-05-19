using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M_ShopStateBt : MonoBehaviour, IPointerClickHandler
{
    public TMPro.TMP_Text mytext;
    public TMPro.TMP_Text othertext;
    // Start is called before the first frame update
    void Start()
    {
    }
   public void OnPointerClick(PointerEventData eventData)
    {
        mytext.color = Color.black;
        mytext.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, -5.0f);
        othertext.color = Color.white;
        othertext.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, +5.0f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
