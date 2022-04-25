using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Item", menuName = "Item/New Item")]
public class SItemData : ScriptableObject
{
    public enum Type
    {
        Consume, Map, ETC
    }

    public string Name;  //¿Ã∏ß
    public string ToolTip;
    public Type ItemType;
    public Sprite Image;
    public bool Countable = false;
}
