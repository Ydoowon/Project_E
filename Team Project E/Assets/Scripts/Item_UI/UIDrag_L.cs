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


    void Start()
    {
        Min.x = myRoot.GetComponent<RectTransform>().sizeDelta.x * 0.5f * Screen.width / 1920.0f;
        Max.x = (1920.0f - myRoot.GetComponent<RectTransform>().sizeDelta.x * 0.5f) * Screen.width / 1920.0f;
        float comp = Screen.width / 1080.0f;
        Min.y = myRoot.GetComponent<RectTransform>().sizeDelta.y * 0.5f * Screen.height / 1080.0f * comp;
        Max.y = (1920.0f - myRoot.GetComponent<RectTransform>().sizeDelta.y * 0.5f) * Screen.height / 1080.0f * comp;
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        DragOffset = (Vector2)myRoot.transform.position - eventData.position;
        this.transform.parent.parent.SetAsLastSibling();
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
