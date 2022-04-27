using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerEnterHandler, IPointerExitHandler
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
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        TMPro.TMP_Text[] ToolTip = SGameManager.instance.ItemToolTip.GetComponentsInChildren<TMPro.TMP_Text>();
        ToolTip[0].text = "<color=red>" + ItemData.Name + "</color>";
        ToolTip[1].text = ItemData.ToolTip;

        switch(ItemData.ItemType)
        {
            case SItemData.Type.Map:
                ToolTip[2].text = "<color=blue> 지도</color> 아이템";
                break;
            case SItemData.Type.Consume:
                ToolTip[2].text = "<color=blue> 더블클릭</color>으로 사용";
                break;
            case SItemData.Type.ETC:
                ToolTip[2].text = "재료 아이템";
                break;

        }
        //

        float x = (Screen.width / 1920.0f) * SGameManager.instance.ItemToolTip.GetComponent<RectTransform>().sizeDelta.x / 2.0f;
        float y = (Screen.width / 1920.0f) * SGameManager.instance.ItemToolTip.GetComponent<RectTransform>().sizeDelta.y / 2.0f;

        SGameManager.instance.ItemToolTip.transform.position = this.transform.position + new Vector3(x, -y, 0f);
        SGameManager.instance.ItemToolTip.SetActive(true);
        
        //포지션 값에 얼마를 넣어줘야 하는 것??
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SGameManager.instance.ItemToolTip.SetActive(false);
    }


    





}
