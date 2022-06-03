using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SStock_Shelves : MonoBehaviour
{
    SItem Item;
    public int myPrice;
    public Transform myItemslot;
    public Animator myItemanim;
    public int realprice;
    [SerializeField]
    bool InUse = false;
    public bool DisplayItem = false;
    // Start is called before the first frame update
    public bool GetInUse()
    {
        return InUse;
    }
    public void SetInUse(bool boolean)
    {
        InUse = boolean;
    }
    public bool GetDisplaying()
    {
        return DisplayItem;
    }

    public void Displaying(GameObject Item)
    {
        if (Item == null || DisplayItem) return;
        /*
        myItem = Item;
        myItem.transform.localPosition = new Vector3(0,1.0f,1.5f);
        myItem.transform.rotation = Quaternion.identity;
        price = myItem.GetComponent<SMap>().GetPrice();
        */

        DisplayItem = true;
    }

    public void OutStockItem()
    {
        InUse = false;
        DisplayItem = false;
    }

    public void SetItem(SItem _item)
    {
        Item = _item;
        _item.transform.SetParent(myItemslot);
        realprice = _item.GetComponent<SItem>().Price;
        myItemanim.SetBool("Activate", true);
    }

}
