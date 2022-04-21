using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ui : MonoBehaviour
{
    bool can = true;
    bool Menushow = true;
    bool Shop = false;
    bool Shopshow = true;
    public event UnityAction Open = null;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<M_popup>().showing += () => Menushow = !Menushow;

        GameObject.Find("Shoprnge").GetComponent<M_ShopOpen>().Shoping += () => Shop = !Shop;
      

        
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Shop)
        {
            if (Shopshow)
            {
                Open.Invoke();
                GameObject obj = Instantiate(Resources.Load("UI/M_Shop"), this.transform) as GameObject;
                obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
                this.GetComponentInChildren<M_shop>().shopOpen += () => Shopshow = true;
                this.GetComponentInChildren<M_shop>().Open += () => Open();
                Shopshow = false;
            }
            else
            {
                this.GetComponentInChildren<M_shop>().Close();
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {       
                if (Menushow && can)
                {
                    if (Shopshow)
                    {
                        this.GetComponentInChildren<M_popup>().Open();
                    }

                }
                else
                {
                    this.GetComponentInChildren<M_menu>().OnClose();
                }
        }
    }
}
