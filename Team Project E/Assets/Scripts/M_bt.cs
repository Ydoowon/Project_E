using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class M_bt : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public TMPro.TMP_Text mytext;
    public Image ItemImage;
    public TMPro.TMP_Text ItemName;
    public TMPro.TMP_Text Cost;
    public M_Buymany PopUp;
    public M_ShopOpen myShop;
    SPlayer _player;
    public SPlayer Player
    {
        get
        {
            if (_player == null)
            {
                _player = myShop.Player;
            }
            return _player;
        }
    }

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

    public void PopUpSetting(int index)
    {
        PopUp.SetItem(ItemImage, ItemName, Cost);
        PopUp.PurchaseIndex = index;
    }
}
