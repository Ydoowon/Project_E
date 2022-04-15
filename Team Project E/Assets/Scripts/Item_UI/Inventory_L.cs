//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Inventory_L : MonoBehaviour
//{
//    #region Singleton
//    public static Inventory_L instance;
//    private void Awake()
//    {
//        if(instance != null)
//        {
//            Destroy(gameObject);
//            return;
//        }
//        instance = this;
//    }
//    #endregion

//    public delegate void OnSlotCountChange(int val);
//    public OnSlotCountChange onSlotCountChange;

//    public delegate void OnChangeItem();
//    public OnChangeItem onChangeItem;

//    List<Item> items = new List<Item>();

//    private int slotCnt;
//    public int SlotCnt
//    {
//        get
//        {
//            return slotCnt;
//        }
//        set
//        {
//            slotCnt = value;
//            onSlotCountChange.Invoke(slotCnt);
//        }
//    }

//    void Start()
//    {
//        SlotCnt = 16;
//    }

//    public bool AddItem(Item _item)
//    {
//        if(items.Count < SlotCnt)
//        {
//            items.Add(_item);
//            onChangeItem.Invoke();
//            return true;
//        }
//        return false;
//    }

//    private void OnCollisionEnter2D(Collider2D collision)
//    {
//        if(collision.CompareTag("Item"))
//        {

//        }
//    }
//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
