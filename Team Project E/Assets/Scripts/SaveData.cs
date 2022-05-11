using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public InventoryData(int _slotindex, int _itemindex, int _itemcount)
    {
        SlotIndex = _slotindex;
        ItemIndex = _itemindex;
        ItemCount = _itemcount;
    }
    public int SlotIndex;
    public int ItemIndex;
    public int ItemCount;
}


[System.Serializable]
public class SaveData
{
    public Vector3 PlayerPos;
    public Vector3 PlayerRot;
    // Start is called before the first frame update
    public int _level;
    public float _exp;
    public float _hp;
    public float _hidepoint;
    public int _gold;

    //Map info
    public Map[] PlayerMapList;
    public int _usingMapNum;
    public InventoryData[] Inventory;
    
    
    //Stock info
    public List<SItem> StockItem = new List<SItem>();

}
