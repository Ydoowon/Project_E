using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_shop : MonoBehaviour
{

    public GameObject layout;
    public event UnityAction shopclose; // 상점이 닫힐때 나타날 함수를 전달.
    public TMPro.TMP_Text Gold;
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

    public void Action(bool open)
    {
        if (open)
        {
            Gold.text = Player.MyStatus.Gold.ToString();
        }
        this.gameObject.SetActive(open);
    }

    public void BuyManyOpen()
    {
        GameObject obj = Instantiate(Resources.Load("UI/M_Buymany"), this.transform) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
        obj.GetComponent<M_Buymany>().layout += () => layout.SetActive(false);
        layout.SetActive(true);
    }
    public void Close()
    {
        shopclose?.Invoke();
        Destroy(this.gameObject);
    }

}
