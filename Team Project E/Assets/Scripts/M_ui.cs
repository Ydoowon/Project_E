using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ui : MonoBehaviour
{
    bool can = true;
    bool show = true;
    bool Shop = false;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<M_popup>().showing += () =>
        {
            if (show)
            {
                show = false;
            }
            else
            {
                show = true;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Shop)
        {          
            GameObject obj = Instantiate(Resources.Load("UI/M_Shop"), this.transform) as GameObject;
            obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {       
                if (show && can)
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
