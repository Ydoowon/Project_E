using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_L : MonoBehaviour
{
    

    public Slot_L[] slots;
    public Transform myContent;

    //Inventory_L inven;
    
    // Start is called before the first frame update
    void Start()
    {
        slots = myContent.GetComponentsInChildren<Slot_L>();
        //inven.onSlotCountChange += SlotChange;
    }
    private void SlotChange(int val)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public bool AddItem(Item _item)
    //{
    //    if(items.Count < SlotCnt)
    //    {
    //        items.Add(_item);
    //        if(onChangeItem != null)  onChangeItem.Invoke();
    //        return true;
    //    }

    //    return false;
    //}

    //void SendITem()    어떤 상황 발생시 전달 해주는 함수
    //{
    //    AddItem(item);
    //}
}
