using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ui : MonoBehaviour
{
    bool Menushow = true; // escŰ�� ������ ���� �޴�â�� ���Դ��� �ȳ��Դ���
    bool Shoprng = false; // Shop�� �� ������ �ִ��� ������
    bool Shopshow = true; // Shop�� ���� �ִ��� �ȿ����ִ���
    bool PlayerShoprng = false; // �÷��̾� ���� ������ �ִ���
    bool PlayerShopshow = true; // �÷��̾� ������ ��Ÿ�� �ִ���

    // Start is called before the first frame update
    void Start()
    {

        GameObject.Find("Shoprnge").GetComponent<M_ShopOpen>().Shoping += () => Shoprng = !Shoprng;
       // GameObject.Find("Conter").GetComponent<M_playerShopOpen>().PlayerShoprng += () => PlayerShoprng = !PlayerShoprng;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        { // NPC ����
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

            //Player ����
             if(PlayerShoprng)
            {
                if (PlayerShopshow)
                {
                    GameObject obj = Instantiate(Resources.Load("UI/M_PlayerShop"), this.transform) as GameObject;
                    obj.transform.position = new Vector3(1370.0f, 540.0f, 0.0f);
                    obj.name = "M_PlayerShop";
                    
                    PlayerShopshow = false;
                    
                }
                else
                {                 
                        PlayerShopshow = true;
                        this.GetComponentInChildren<M_PlayerShop>()?.Close();
                }

            }           

        }


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


    }
}
