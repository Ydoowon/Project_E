using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ui : MonoBehaviour
{
    bool Menushow = true; // esc키를 눌러서 나온 메뉴창이 나왔는지 안나왔는지
    bool Shoprng = false; // Shop을 열 범위에 있는지 없는지
    bool Shopshow = true; // Shop이 열려 있는지 안열려있는지
    bool PlayerShoprng = false;
    bool PlayerShopshow = true;

    
    // Start is called before the first frame update
    void Start()
    {

        GameObject.Find("Shoprnge").GetComponent<M_ShopOpen>().Shoping += () => Shoprng = !Shoprng;            
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { // NPC 상점
            if (Shoprng)
            {
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
            }
           /* else if(PlayerShoprng)
            {
                if (PlayerShopshow)
                {
                    PlayerShopshow = false;
                    this.GetComponentInChildren<M_PlayerShop>().ISOpen();
                }
                else
                {
                    PlayerShopshow = true;
                    this.GetComponentInChildren<M_PlayerShop>().ISClose();
                }

            }*/
            
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
