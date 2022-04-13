using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct Iteminfo
{
    public string name;  //이름
    public bool countable;   // 개수가 있는 아이템인가 체크
    public int quantity;   // 개수
    public int price;   // 가격

    public enum Type
    {
        Equipment, Consume, Stamp, Map
    }

}
public class Item : MonoBehaviour
{
    public Iteminfo myIteminfo;
    public LayerMask TouchMask;

    private void OnTriggerEnter(Collider other)
    {

        if ((TouchMask & (1 << other.gameObject.layer)) > 0)
        {
            
        }
    }
}
