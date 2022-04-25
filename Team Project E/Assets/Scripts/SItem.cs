using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public SItemData ItemData;
    Vector2 DragOffset;
    Transform curParent = null;
    public int Price;

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.GetComponent<Image>().raycastTarget = false;
        curParent = this.transform.parent;
        curParent.gameObject.GetComponent<SItemSlot>().Itemcnt.gameObject.SetActive(false);
        this.transform.SetParent(curParent.parent);
        DragOffset = (Vector2)this.transform.position - eventData.position;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = DragOffset + eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(curParent);
        this.transform.localPosition = Vector2.zero;
        this.GetComponent<Image>().raycastTarget = true;
        this.transform.SetAsFirstSibling();
        curParent.GetComponent<SItemSlot>().Itemcnt.gameObject.SetActive(ItemData.Countable);
    }

    public void ChangeParent(Transform P)
    {
        SItem tempItem = P.GetComponentInChildren<SItem>();
        if (tempItem != null)
        {
            tempItem.ChangeParent(curParent);
            tempItem.transform.SetAsFirstSibling();
        }

        curParent = P;
        this.transform.SetParent(curParent);
        this.transform.localPosition = Vector3.zero;
    }

    public Transform GetCurParent()
    {
        return curParent;
    }


    

    // Start is called before the first frame update




}
