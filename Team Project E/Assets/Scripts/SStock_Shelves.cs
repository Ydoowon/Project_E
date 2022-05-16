using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SStock_Shelves : MonoBehaviour
{
    public GameObject myItem;
    public int price;
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

        myItem = Item;
        myItem.transform.localPosition = new Vector3(0,1.0f,1.5f);
        myItem.transform.rotation = Quaternion.identity;
        price = myItem.GetComponent<SMap>().GetPrice();

        DisplayItem = true;
    }

    public void OutStockItem()
    {
        if (myItem == null) return;

        Destroy(myItem.gameObject);
        myItem = null;
        price = 0;
        InUse = false;
        DisplayItem = false;
    }
}
