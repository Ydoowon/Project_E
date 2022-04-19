using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMap : MonoBehaviour
{
    [SerializeField]
    int price;  // 지도 가격

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
        return price;
    }
    public void SetPrice(int _price)
    {
        price = _price;
    }
}
