using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDrag_L : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 DragOffset = Vector2.zero;
    Transform Curparent = null;
    //float clickTime = 0;
    //public GameObject myPlayer = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Curparent = this.transform.parent;
        this.GetComponent<Image>().raycastTarget = false;
        DragOffset = (Vector2)this.transform.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = DragOffset + eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<Image>().raycastTarget = true;
    }
}
