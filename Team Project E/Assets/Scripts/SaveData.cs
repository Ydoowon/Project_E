using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public InventoryData(int _slotindex, int _itemindex, int _itemcount, int _itemprice )
    {
        SlotIndex = _slotindex;
        ItemIndex = _itemindex;
        ItemCount = _itemcount;
        ItemPrice = _itemprice;
    }
    public int SlotIndex;
    public int ItemIndex;
    public int ItemCount;
    public int ItemPrice;
}


[System.Serializable]
public class SaveData
{
    public Vector3 PlayerPos;
    public Vector3 PlayerRot;
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

    //time info
    public string SavingTime;


}
