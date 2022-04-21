using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SMap : MonoBehaviour
{
    public Item item;
    public Map MapData;

    // 플레이어 상점 테스트를 위한 임시 스크립트입니다.
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
