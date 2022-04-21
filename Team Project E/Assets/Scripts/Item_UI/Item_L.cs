using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item_L : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
    public Transform mySlotGroup = null;
    //float clickTime = 0;
    //public GameObject myPlayer = null;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Curparent = this.transform.parent;
        this.transform.SetParent(Curparent.parent);
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
        this.transform.SetParent(Curparent);
        this.transform.localPosition = Vector3.zero;
    }

    public void ChangeParent(Transform p, bool Swap = true)
    {
        if (Swap)
        {
            Item_L tempItem = p.GetComponentInChildren<Item_L>();
            if (tempItem != null)
            {
                tempItem.ChangeParent(Curparent, false);
            }
        }
        Curparent = p;
        this.transform.SetParent(Curparent);
        this.transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = Instantiate(Resources.Load("Item_Inven/potion"), mySlotGroup) as GameObject;

        Destroy(gameObject);
    }

    //void OnMouseDoubleClick()
    //{
    //    float HP = myPlayer.GetComponent<SPlayer>().Hp;
    //    if (HP == 100) return;
    //    else
    //    {
    //        if(HP > 80)
    //        {
    //            HP = 100;
    //        }
    //        else
    //        {
    //            HP = +20;
    //        }
            
    //    }
    //    Destroy(gameObject);

    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    if((Time.time - clickTime) < 0.3f)
    //    {
    //        OnMouseDoubleClick();
    //        clickTime = -1;
    //    }
    //    else
    //    {
    //        clickTime = Time.time;
    //    }
    //}


}
