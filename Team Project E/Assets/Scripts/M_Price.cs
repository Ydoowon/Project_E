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

    public GameObject PastitemParent = null;

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
 
    public  void Bt(int index)
    {
        Number.Add(index);
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
        if (myImageset != null)
        {
            myImageset.transform.GetChild(2).gameObject.SetActive(false);
            myImageset.transform.GetChild(3).gameObject.GetComponent<SItem>().ChangeParent(PastitemParent.transform);
        }

            Destroy(this.gameObject);
    }

    public void OK()
    {
               
       if( myImageset == null)
        {
            this.transform.parent.GetComponentInChildren<M_shopslot>().Write(price);
            Destroy(this.gameObject);
            return;
        }
        myImageset.InputGold(price);
        myImageset = null;
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
