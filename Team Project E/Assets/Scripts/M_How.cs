using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_How : MonoBehaviour
{
    public GameObject Shop;
    public GameObject PlayerShop;
    float Shophight = 580;
    float PlaterShophight = 400;
    public bool shop = false;
    public bool playershop = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shop) displayknowhow(Shophight, Shop);
        if(playershop) displayknowhow(PlaterShophight, PlayerShop);
    }
    void displayknowhow(float hight,GameObject pos) 
    {
        Vector3 Pos = Camera.main.WorldToScreenPoint(pos.transform.position);
        Pos.y = Pos.y + hight;
        this.GetComponent<RectTransform>().anchoredPosition = Pos;
    }
}
