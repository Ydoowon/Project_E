using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SMap : MonoBehaviour
{
    public Item item;
    public Map MapData;

    // �÷��̾� ���� �׽�Ʈ�� ���� �ӽ� ��ũ��Ʈ�Դϴ�.
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPrice()
    {
        return MapData.GetPrice();
    }
    public void SetPrice(int _price)
    {
        MapData.SetPrice(_price);
    }


}
