using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ui : MonoBehaviour
{
    //bool Menushow = true; // esc키를 눌러서 나온 메뉴창이 나왔는지 안나왔는지
    bool PlayerShoprng = false; // 플레이어 상점 범위에 있는지
    public bool PlayerShopshow = true; // 플레이어 상점이 나타나 있는지

    public GameObject Shoprang;
    public GameObject Counter;
    public GameObject PlaterShop;

    // Start is called before the first frame update
    void Start()
    {

        //Shoprang.GetComponent<M_ShopOpen>().Shoping += () => Shoprng = !Shoprng;
        //Counter.GetComponent<M_playerShopOpen>().PlayerShoprng += () => PlayerShoprng = !PlayerShoprng;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Shoprng)
        { // NPC 상점
            if (Input.GetKeyDown(KeyCode.E))
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
        }
        */
        //Player 상점
        if (PlayerShoprng)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (PlayerShopshow)
                {
                    PlaterShop.SetActive(true);
                    PlayerShopshow = false;

                }
                else
                {

                    PlaterShop.GetComponent<M_PlayerShop>().Close();
                }
            }
        }


        /*

        if (Input.GetKeyDown(KeyCode.Escape))
        {       
                if (Menushow)
                {
                    if (Shopshow && PlayerShopshow)
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
        */


    }
}
