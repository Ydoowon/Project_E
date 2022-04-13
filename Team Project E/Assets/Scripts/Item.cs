using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct Iteminfo
{
    public string name;  //�̸�
    public bool countable;   // ������ �ִ� �������ΰ� üũ
    public int quantity;   // ����
    public int price;   // ����

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
