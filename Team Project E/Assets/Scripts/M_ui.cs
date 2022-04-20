using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ui : MonoBehaviour
{
    public GameObject ShopingRange;
    bool can = true;
    bool Menushow = true;
    bool Shop = false;
    bool Shopshow = true;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<M_popup>().showing += () =>
        {
            if (Menushow)
            {
                Menushow = false;
            }
            else
            {
                Menushow = true;
            }
        };
        GameObject.Find("Shoprnge").GetComponent<M_ShopOpen>().Shoping += () =>
        {
            if (Shop)
            {
                Shop = false;
            }
            else
            {
                Shop = true;
            }
      
        };
        
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Shop)
        {
            if (Shopshow)
            {
                GameObject obj = Instantiate(Resources.Load("UI/M_Shop"), this.transform) as GameObject;
                obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
                this.GetComponentInChildren<M_shop>().shopOpen += () => Shopshow = true;
                Shopshow = false;
            }
            else
            {
                this.GetComponentInChildren<M_shop>().Close();               
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {       
                if (Menushow && can && !Shop)
                {
                    this.GetComponentInChildren<M_popup>().Open();

                }
                else
                {
                    this.GetComponentInChildren<M_menu>().OnClose();
                }
        }
    }
}
