using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ui : MonoBehaviour
{
    bool Menushow = true; // esc???? ?????? ???? ???????? ???????? ??????????
    bool Shopsshow;
    bool PlayerShoprng = false; // ???????? ???? ?????? ??????
   public bool PlayerShopshow = true; // ???????? ?????? ?????? ??????

    public GameObject Shoprang;
    public GameObject Counter;

    // Start is called before the first frame update
    void Start()
    {
        Shopsshow = Shoprang.GetComponent<M_ShopOpen>().Shopsshow;
        PlayerShopshow = Counter.GetComponent<M_playerShopOpen>().PlayerShopshow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {       
                if (Menushow)
                {
                    if (Shopsshow && PlayerShopshow)
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
