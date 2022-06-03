using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class M_BuyChek : MonoBehaviour
{
    public event UnityAction Layout = null;
    public event UnityAction BuyingItem = null;
    //public UnityAction 
    public TMPro.TMP_Text Message;
    M_ShopOpen myShop;
    SPlayer _player = null;
    public SPlayer Player
    {
        get
        {
            if (_player == null)
                _player = myShop.Player;

            return _player;
        }
    }

    public void Buy()
    {
        BuyingItem?.Invoke();
        BuyingItem = null;
        this.gameObject.SetActive(false);

    }
    public void Cencel()
    {
        BuyingItem = null;
        this.gameObject.SetActive(false);
    }
}
