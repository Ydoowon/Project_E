using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot_L : MonoBehaviour , IDropHandler
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Item_L Invenitem = eventData.pointerDrag.GetComponent<Item_L>();
        //StoreItem_L storeitem = eventData.pointerDrag.GetComponent<StoreItem_L>();
        if (Invenitem != null)
        {
            Invenitem.ChangeParent(this.transform);

        }

        //if (storeitem != null)
        //{
        //    storeitem.ChangeParent(this.transform);

        //}
    }

}
