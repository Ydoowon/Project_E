using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ui : MonoBehaviour
{
    //bool Menushow = true; // esc키를 눌러서 나온 메뉴창이 나왔는지 안나왔는지
    public GameObject Shoprang;
    public GameObject Counter;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
 

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
