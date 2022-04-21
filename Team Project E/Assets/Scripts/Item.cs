using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Map", menuName = "New Item/Map")]
public class Item : ScriptableObject
{
    public string MapName;  //�̸�
    public string ToolTip;

    public Sprite MapImage;


}
