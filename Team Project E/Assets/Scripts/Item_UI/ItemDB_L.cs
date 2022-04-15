using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTyep
{
    Using, Equipment
}


[System.Serializable]
public class ItemDB
{
    public ItemTyep itemTyep;
    public string itemName;
    public Sprite itemImage;

    public bool Use()
    {
        return false;
    }
}
