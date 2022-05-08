using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_Price : MonoBehaviour
{
    public M_imageset myImageset;
    public TMPro.TMP_Text lable;
    public int price = 0;
    List<int> Number = new List<int>();
    public GameObject myStor;
    int tempGold;

    void BT()
    {
        if(Number[0] == 0)
        {
            price = 0;
            lable.text = "" + price;
            Number.RemoveAt(0);
        }
        else
        {
            if (Number.Count > 9)
            {
                Number.RemoveAt(9);
            }
            int temp = 0;
            int pos = Number.Count - 1;
            for (int i = 0; i < Number.Count; i++)
            {
                temp += Number[i] * (int)Mathf.Pow(10, pos);
                pos -= 1;
            }
            price = temp;
            lable.text = "" + price;
        }
    }
 
    public  void Bt1()
    {
        Number.Add(1);
        BT();
    }
    public void Bt2()
    {
        Number.Add(2);
        BT();
    }
    public void Bt3()
    {
        Number.Add(3);
        BT();
    }
    public void Bt4()
    {
        Number.Add(4);
        BT();
    }
    public void Bt5()
    {
        Number.Add(5);
        BT();
    }
    public void Bt6()
    {
        Number.Add(6);
        BT();
    }
    public void Bt7()
    {
        Number.Add(7);
        BT();
    }
    public void Bt8()
    {
        Number.Add(8);
        BT();
    }
    public void Bt9()
    {
        Number.Add(9);
        BT();
    }
    public void Bt0()
    {
        Number.Add(0);
        BT();
    }


    public void Backspace()
    {    
        if(Number.Count == 1)
        {
            price = 0;
            lable.text = "" + price;
            Number.RemoveAt(Number.Count - 1);
        }
        if (Number.Count == 0)
        {
            price = 0;
            lable.text = "" + price;
        }
        else
        {
            Number.RemoveAt(Number.Count - 1);
            BT();
        }
    }

    public void cancel()
    {
        Destroy(this.gameObject);
    }
    public void OK()
    {
        
        GameObject.Find("M_PlayerShop").GetComponentInChildren<M_shopslot>().tempGold = price;
        myImageset.InputGold(price);
        Destroy(this.gameObject);
    }
        // Start is called before the first frame update
    void Start()
    {
        lable.text = "" + price;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
