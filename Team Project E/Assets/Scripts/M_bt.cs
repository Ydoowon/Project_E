using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M_bt : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    Button mybutton;
    [SerializeField] GameObject layout; 
    private void Start()
    {
        mybutton = this.GetComponent<Button>();
        if(mytext.text == "Shop Close")
        {
            mybutton.interactable = false;
            layout.SetActive(true);
            mytext.color = Color.black;
            mytext.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, -5.0f);
        }
    }
    public TMPro.TMP_Text othertext = null;
    public TMPro.TMP_Text mytext;
    public void OnPointerDown(PointerEventData eventData)
    {
        mytext.color = Color.black;
        mytext.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, -5.0f);
        if (othertext != null)
        {
            layout.SetActive(true);
            mybutton.onClick.Invoke();
            mybutton.interactable = false;
            othertext.color = Color.white;
            othertext.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, +5.0f);
            othertext.GetComponentInParent<Button>().interactable = true;
            othertext.GetComponentInParent<M_bt>().layout.SetActive(false);
        }

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (othertext != null) return;
        mytext.color = Color.white;
        mytext.GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, +5.0f);
       
    }




}
