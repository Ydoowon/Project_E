using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M_shopslot : MonoBehaviour
{
    public GameObject BT1;
    public GameObject BT2;
    public GameObject BT3;
    public GameObject BT4;
    public GameObject BT5;
    public GameObject BT6;
    public GameObject BT7;
    public GameObject BT8;
    public TMPro.TMP_Text gold1;
    public TMPro.TMP_Text gold2;
    public TMPro.TMP_Text gold3;
    public TMPro.TMP_Text gold4;
    public TMPro.TMP_Text gold5;
    public TMPro.TMP_Text gold6;
    public TMPro.TMP_Text gold7;
    public TMPro.TMP_Text gold8;
    public int tempGold = 0;

    static int Gold1 = 0;
    static int Gold2 = 0;
    static int Gold3 = 0;
    static int Gold4 = 0;
    static int Gold5 = 0;
    static int Gold6 = 0;
    static int Gold7 = 0;
    static int Gold8 = 0;

    public int i = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void destoryBT1()
    {
        BT1.SetActive(false);
        gold1.text = "" + 0;
    }
    public void destoryBT2()
    {
        BT2.SetActive(false);
        gold2.text = "" + 0;
    }
    public void destoryBT3()
    {
        BT3.SetActive(false);
        gold3.text = "" + 0;
    }
    public void destoryBT4()
    {
        BT4.SetActive(false);
        gold4.text = "" + 0;
    }
    public void destoryBT5()
    {
        BT5.SetActive(false);
        gold5.text = "" + 0;
    }
    public void destoryBT6()
    {
        BT6.SetActive(false);
        gold6.text = "" + 0;
    }
    public void destoryBT7()
    {
        BT7.SetActive(false);
        gold7.text = "" + 0;
    }
    public void destoryBT8()
    {
        BT8.SetActive(false);
        gold8.text = "" + 0;
    }



    public void GoldReset1()
    {
        i = 1;
      GameObject obj = Instantiate(Resources.Load("UI/M_GlodSet"), this.transform.parent) as GameObject;
      obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
        
    }
    public void GoldReset2()
    {
        i = 2;
        GameObject obj = Instantiate(Resources.Load("UI/M_GlodSet"), this.transform.parent) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

    }
    public void GoldReset3()
    {
        i = 3;
        GameObject obj = Instantiate(Resources.Load("UI/M_GlodSet"), this.transform.parent) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

    }
    public void GoldReset4()
    {
        i = 4;
        GameObject obj = Instantiate(Resources.Load("UI/M_GlodSet"), this.transform.parent) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

    }
    public void GoldReset5()
    {
        i = 5;
        GameObject obj = Instantiate(Resources.Load("UI/M_GlodSet"), this.transform.parent) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

    }
    public void GoldReset6()
    {
        i = 6;
        GameObject obj = Instantiate(Resources.Load("UI/M_GlodSet"), this.transform.parent) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

    }
    public void GoldReset7()
    {
        i = 7;
        GameObject obj = Instantiate(Resources.Load("UI/M_GlodSet"), this.transform.parent) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

    }
    public void GoldReset8()
    {
        i = 8;
        GameObject obj = Instantiate(Resources.Load("UI/M_GlodSet"), this.transform.parent) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

    }

    public void Print()
    {
        change();
        gold1.text = "" + Gold1;
        gold2.text = "" + Gold2;
        gold3.text = "" + Gold3;
        gold4.text = "" + Gold4;
        gold5.text = "" + Gold5;
        gold6.text = "" + Gold6;
        gold7.text = "" + Gold7;
        gold8.text = "" + Gold8;
    }
     void change()
    {
        switch (i)
        {
            case 1:
                Gold1 = tempGold;
                break;
            case 2:
                Gold2 = tempGold;
                break;
            case 3:
                Gold3 = tempGold;
                break;
            case 4:
                Gold4 = tempGold;
                break;
            case 5:
                Gold5 = tempGold;
                break;
            case 6:
                Gold6 = tempGold;
                break;
            case 7:
                Gold7 = tempGold;
                break;
            case 8:
                Gold8 = tempGold;
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {        
        Print();
    }
}
