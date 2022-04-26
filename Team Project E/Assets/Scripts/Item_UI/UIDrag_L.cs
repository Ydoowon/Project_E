using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDrag_L : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform myRoot;
    Vector2 DragOffset = Vector2.zero;

    Vector2 Min = Vector2.zero;
    Vector2 Max = Vector2.zero;
    public void OnBeginDrag(PointerEventData eventData)
    {
        DragOffset = (Vector2)myRoot.transform.position - eventData.position;
        Min.x = myRoot.GetComponent<RectTransform>().sizeDelta.x / 2;
        Min.y = myRoot.GetComponent<RectTransform>().sizeDelta.y / 2;
        Max.x = Screen.width - Min.x;
        Max.y = Screen.height - Min.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        myRoot.position = DragOffset + eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 pos = DragOffset + eventData.position;
        pos.x = Mathf.Clamp(pos.x, Min.x, Max.x);
        pos.y = Mathf.Clamp(pos.y, Min.y, Max.y);
        myRoot.transform.position = pos;
    }
}
