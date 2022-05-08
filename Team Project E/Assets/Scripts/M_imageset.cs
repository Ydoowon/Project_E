using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class M_imageset : MonoBehaviour,IDropHandler
{
    public GameObject disapear;
    public SItem myItem;
    // Start is called before the first frame update
    void Start()
    {
    }

  public  void OnDrop(PointerEventData eventData)
    {
        SItem DragedItem = eventData.pointerDrag.GetComponent<SItem>(); // 아이템 끌고온것
        if (DragedItem == null) return;
        SItemSlot Pastslot = DragedItem.GetCurParent().GetComponent<SItemSlot>();
        if (DragedItem.ItemData.ItemType == SItemData.Type.Map)
        {
            if (this.transform.childCount == 4) return;

            Pastslot.myItem = null;
            DragedItem.ChangeParent(this.transform);
            DragedItem.transform.SetAsLastSibling();
            DragedItem.ableDrag = false;
            myItem = DragedItem;
            disapear.SetActive(true);
            GameObject obj = Instantiate(Resources.Load("UI/M_GlodSet"), this.transform.parent.parent.parent) as GameObject;
            obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
            obj.GetComponent<M_Price>().myImageset = this;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void InputGold(int _gold)
    {
        TMPro.TMP_Text Gold = disapear.GetComponentInChildren<TMPro.TMP_Text>();
        Gold.text = _gold.ToString();
    }
}
