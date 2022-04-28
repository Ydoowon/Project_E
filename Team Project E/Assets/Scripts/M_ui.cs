using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ui : MonoBehaviour
{
    bool Menushow = true; // esc키를 눌러서 나온 메뉴창이 나왔는지 안나왔는지
    bool Shoprng = false; // Shop을 열 범위에 있는지 없는지
   public bool Shopshow = true; // Shop이 열려 있는지 안열려있는지

    public GameObject ShopRnge;
    // Start is called before the first frame update
    void Start()
    {

        ShopRnge.GetComponent<M_ShopOpen>().Shoprng += () => Shoprng = !Shoprng;
        
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Shoprng)
        { // NPC 상점
            if (Shopshow)
            {
                GameObject obj1 = Instantiate(Resources.Load("UI/M_Shop"), this.transform) as GameObject;
                obj1.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
                this.GetComponentInChildren<M_shop>().shopOpen += () => Shopshow = true;
                Shopshow = false;
            }
            else
            {
                Shopshow = true;
                this.GetComponentInChildren<M_shop>()?.Close();
            }
         // Player 상점


        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {       
                if (Menushow)
                {
                    if (Shopshow)
                    {
                    GameObject obj2 = Instantiate(Resources.Load("UI/M_Menu"), this.transform) as GameObject;
                    obj2.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
                    obj2.GetComponent<M_menu>()?.Open();
                    Menushow = false;
                    }

                }
                else
                {
                    this.GetComponentInChildren<M_menu>()?.Close();
                    Menushow = true;
                }

        }


    }
}
