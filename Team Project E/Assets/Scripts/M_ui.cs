using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ui : MonoBehaviour
{
    bool Menushow = true; // escŰ�� ������ ���� �޴�â�� ���Դ��� �ȳ��Դ���
    bool Shoprng = false; // Shop�� �� ������ �ִ��� ������
    bool Shopshow = true; // Shop�� ���� �ִ��� �ȿ����ִ���


    // Start is called before the first frame update
    void Start()
    {

        GameObject.Find("Shoprnge").GetComponent<M_ShopOpen>().Shoping += () => Shoprng = !Shoprng;            
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Shoprng)
        { // NPC ����
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
         // Player ����


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
