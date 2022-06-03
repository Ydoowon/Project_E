using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ShopOpen : MonoBehaviour
{
    public TMPro.TMP_Text Message;
    public GameObject HOW;
    public M_shop Shop;
    public GameObject UI;
    public event UnityAction Onrange = null; // ������ bool���� ����
    bool IsOpen = false;
    SPlayer _player;
    public SPlayer Player
    {
        get { return _player; }
        set { _player = value; }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HOW.SetActive(true);
            Message.text = "EŰ�� ���� ��������";
            Player = other.GetComponent<SPlayer>();
            /*
            Shop.Player= Player;
            for(int i =0; i<ShopMenu.Length; i++)
            {
                ShopMenu[i].Player = Player;
            }
            Shop.Gold.text = Player.MyStatus.Gold.ToString();
            */
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HOW.SetActive(false);
            CloseShop();
            Player = null;
        }
    }

    void Update()
    {
        if (Player != null && Input.GetKeyDown(KeyCode.E))
        {
            IsOpen = !IsOpen;
            Shop.Action(IsOpen);
        }
    }

    public void CloseShop()
    {
        IsOpen = false;
        Shop.Action(false);
    }
}
