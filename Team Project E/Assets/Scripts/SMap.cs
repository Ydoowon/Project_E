using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SMap : SItem
{
    //public Item item;
    public Map MapData;
    


    public int GetPrice()
    {
        return MapData.GetPrice();
    }
    public void SetPrice(int _price)
    {
        MapData.SetPrice(_price);
    }


}
