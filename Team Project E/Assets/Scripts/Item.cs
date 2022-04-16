using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string ItemName;  //¿Ã∏ß
    public Type ItemType;

    public Sprite ItemImage;
    public GameObject ItemPrefab;

    public enum Type
    {
        Equipment, Consume, Stamp, Map
    }

}
