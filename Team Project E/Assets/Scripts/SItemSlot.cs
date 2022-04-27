using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SItemSlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public TMPro.TMP_Text Itemcnt;
    public SItem myItem = null;
    public int ItemCount = 0;
    float ClickTime = 0.0f;
    [SerializeField]
    GameObject Player;

    public void OnDrop(PointerEventData eventData)
    {
        SItem DragedItem = eventData.pointerDrag.GetComponent<SItem>();
        if (DragedItem == null) return;

        SItemSlot Pastslot = DragedItem.GetCurParent().GetComponent<SItemSlot>();
        
        if (myItem != null)
        {   //같은 아이템을 옮기는 경우
            if (myItem.ItemData.Name == DragedItem.ItemData.Name && myItem.ItemData.Countable)
            {
                // 아이템 전부를 옮기는게 가능한 경우
                if (ItemCount + Pastslot.ItemCount <= 10) 
                {
                    ItemCount += Pastslot.ItemCount;
                    Itemcnt.text = ItemCount.ToString();
                    Pastslot.myItem = null;
                    Pastslot.ItemCount = 0;
                    Destroy(DragedItem.gameObject);
                }
                else // 최대치까지만 옮겨야 하는 경우
                {
                    Pastslot.ItemCount -= (10 - ItemCount);
                    Pastslot.Itemcnt.gameObject.SetActive(true);
                    Pastslot.Itemcnt.text = Pastslot.ItemCount.ToString();
                    ItemCount = 10;
                    Itemcnt.text = ItemCount.ToString();
                }
                return;
            }
            else // 다른 아이템인 경우
            {
                SItem tempItem = myItem;
                int tempItemCount = ItemCount;
                // 서로 바꿔준다.
                SlotSetting(this, Pastslot.ItemCount, DragedItem);
                SlotSetting(Pastslot, tempItemCount, tempItem);
                DragedItem.ChangeParent(this.transform);
                return;
            }
            
        }
        // 슬롯에 아이템이 없었던 경우

        SlotSetting(this, Pastslot.ItemCount, DragedItem);
        Pastslot.myItem = null;
        Pastslot.ItemCount = 0;
        DragedItem.ChangeParent(this.transform);

    }

    public void SlotSetting(SItemSlot Slot, int _ItemCount, SItem Changing_Item)
    {
        Slot.myItem = Changing_Item;
        Slot.ItemCount = _ItemCount;
        Slot.Itemcnt.gameObject.SetActive(Slot.myItem.ItemData.Countable);
        Slot.Itemcnt.text = Slot.ItemCount.ToString();
    }

    public void UpdateItem(SItem Item, int count, bool cntSlot)
    {
        if(myItem == null)
        myItem = Item;

        ItemCount += count;
        Itemcnt.text = ItemCount.ToString();
        Itemcnt.gameObject.SetActive(cntSlot);
    }
    public void RemoveItem(int Count = 1)
    {
        if (myItem == null) return;

        ItemCount -= Count;
        if(ItemCount == 0)
        {
            Itemcnt.text = ItemCount.ToString();
            Itemcnt.gameObject.SetActive(false);
            GameObject myItemObj = this.GetComponentInChildren<SItem>().gameObject;
            Destroy(myItemObj);
            myItem = null;
        }
        else
        {
            Itemcnt.text = ItemCount.ToString();
        }

    }
    // 소비아이템 사용하는 함수(포션)
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Time.time - ClickTime) < 0.3f)
        {
            if (myItem != null)
            {
                if (myItem.GetComponent<SPotion>() != null)
                {
                    OnMouseDoubleClick(Player);
                }
                else
                {
                    Debug.Log(myItem.name + " 은 사용할 수 없는 아이템입니다.");
                }
            }
            ClickTime = -1.0f;
            
        }
        else
        {
            ClickTime = Time.time;
        }
    }
    void OnMouseDoubleClick(GameObject Target)
    {
        if (myItem.GetComponent<SPotion>() == null) return;

        bool isEffected = false;

        SPotion myPotion = myItem.GetComponent<SPotion>();
        if (myPotion.HP != 0) // 회복량이 0이 아니라면
        {
            float Target_HP = Target.GetComponent<SPlayer>().MyStatus.HP;
            if (Target_HP != 100.0f)
            {
                Target.GetComponent<SPlayer>().HealingHP(myPotion.HP);
                isEffected = true;
            }
        }
        if (myPotion.HidePoint != 0) // 회복량이 0이 아니라면
        {

            float Target_HidePoint = Target.GetComponent<SPlayer>().MyStatus.Hidepoint;
            if (Target_HidePoint != 100.0f) 
            {
                Target.GetComponent<SPlayer>().HealingHidePoint(myPotion.HidePoint);
                isEffected = true;
            }
        }
        if(isEffected) // 포션이 효과가 있었다면(hp가 가득차거나 hidepoint가 가득차서 사용되지 않은 경우가 아니라면)
        RemoveItem();
    }

    
}
