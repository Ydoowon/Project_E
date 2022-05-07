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
        {   //���� �������� �ű�� ���
            if (myItem.ItemData.Name == DragedItem.ItemData.Name && myItem.ItemData.Countable)
            {
                // ������ ���θ� �ű�°� ������ ���
                if (ItemCount + Pastslot.ItemCount <= 10) 
                {
                    ItemCount += Pastslot.ItemCount;
                    Itemcnt.text = ItemCount.ToString();
                    Pastslot.myItem = null;
                    Pastslot.ItemCount = 0;
                    Destroy(DragedItem.gameObject);
                }
                else // �ִ�ġ������ �Űܾ� �ϴ� ���
                {
                    Pastslot.ItemCount -= (10 - ItemCount);
                    Pastslot.Itemcnt.gameObject.SetActive(true);
                    Pastslot.Itemcnt.text = Pastslot.ItemCount.ToString();
                    ItemCount = 10;
                    Itemcnt.text = ItemCount.ToString();
                }
                return;
            }
            else // �ٸ� �������� ���
            {
                SItem tempItem = myItem;
                int tempItemCount = ItemCount;
                // ���� �ٲ��ش�.
                SlotSetting(this, Pastslot.ItemCount, DragedItem);
                SlotSetting(Pastslot, tempItemCount, tempItem);
                DragedItem.ChangeParent(this.transform);
                return;
            }
            
        }
        // ���Կ� �������� ������ ���

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
            SGameManager.instance.ItemToolTip.SetActive(false);
        }
        else
        {
            Itemcnt.text = ItemCount.ToString();
        }

    }
    // �Һ������ ����ϴ� �Լ�(����)
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Time.time - ClickTime) < 0.3f)
        {
            if (myItem != null)
            {
                OnMouseDoubleClick(Player);
            }
            else
            {
                Debug.Log(myItem.name + " �� ����� �� ���� �������Դϴ�.");
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
        if (myItem.GetComponent<SItem>().UsingItem(Target) == true)
            RemoveItem();
    }
}
