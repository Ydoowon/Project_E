using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class M_Buymany : MonoBehaviour
{
    public event UnityAction layout = null;
    int many = 1;
    int MAX = 50;
    public TMPro.TMP_Text MANY;
    // Start is called before the first frame update
    public int PurchaseIndex = 999;
    public Image ItemImage;
    public TMPro.TMP_Text Item_name;
    public TMPro.TMP_Text Item_cost;
    int origin_cost = 0;
    bool ablepurchase = false;
    public M_ShopOpen myShop;
    public TMPro.TMP_Text PlayerGold;
    [SerializeField]
    M_BuyChek CheckPopUp;

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


    public void SetItem(Image itemimage, TMPro.TMP_Text itemname, TMPro.TMP_Text itemcost)
    {
        many = 1;
        MANY.text = many.ToString();
        ItemImage.sprite = itemimage.sprite;
        Item_name.text = itemname.text;
        origin_cost = System.Int32.Parse(itemcost.text);
        Item_cost.text = origin_cost + " x " + many + " = ";
        if (Player.MyStatus.Gold < origin_cost * many)
        {
            Item_cost.text += ("<color=red>" + origin_cost * many);
            ablepurchase = false;
        }
        else
        {
            ablepurchase = true;
            Item_cost.text += origin_cost * many;
        }
    }
    public void Up()
    {
        if (many < MAX)
        {
            many += 1;
            MANY.text = many.ToString();

        }
        else
        {
            many = 1;
            MANY.text = many.ToString();
        }
        Item_cost.text = origin_cost + " x " + many + " = ";
        if (_player.MyStatus.Gold < origin_cost * many)
        {
            Item_cost.text += ("<color=red>" + origin_cost * many);
            ablepurchase = false;
        }
        else
        {
            Item_cost.text += origin_cost * many;
            ablepurchase = true;
        }
    }
    public void Down()
    {
        if (many > 1)
        {
            many -= 1;
            MANY.text = "" + many;
        }
        else
        {
            many = MAX;
            MANY.text = "" + many;
        }
        Item_cost.text = origin_cost + " x " + many + " = ";
        if (_player.MyStatus.Gold < origin_cost * many)
        {
            Item_cost.text += ("<color=red>" + origin_cost * many);
            ablepurchase = false;
        }
        else
        {
            Item_cost.text += origin_cost * many;
            ablepurchase = true;
        }
    }
    public void Buy()
    {
        if (PurchaseIndex > SGameManager.instance.Itemlist.Length) return;
        if (!ablepurchase)
        {
            SGameManager.instance.ShowMessage("골드가 부족합니다."); // 현재 canvas 순서 문제로 안보임
            return;
        }
        if (_player.myUIManager.AddAvailable(SGameManager.instance.Itemlist[PurchaseIndex], many))
        {
            CheckPopUp.gameObject.SetActive(true);
            CheckPopUp.Message.text = "<color=blue>" + many * origin_cost + "</color> 골드 입니다." + "\n" + "정말로 구매하시겠습니까?";
            CheckPopUp.BuyingItem += Buying;
        }
    }

    public void Buying()
    {
        Player.MyStatus.Gold -= many * origin_cost;
        Player.myUIManager.AddItem(SGameManager.instance.Itemlist[PurchaseIndex], many);
        PlayerGold.text = Player.MyStatus.Gold.ToString();
    }
    public void Cancel()
    {
        layout.Invoke();
        Destroy(this.gameObject);
    }
}
