using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ShopOpen : MonoBehaviour
{
    public TMPro.TMP_Text How;
    public GameObject HOW;
    public bool Shopsshow = false;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        HOW.SetActive(true);
        How.text = "상점열기";

    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Shopsshow)
            {
                GameObject obj1 = Instantiate(Resources.Load("UI/M_Shop"), Canvas.transform) as GameObject;
                obj1.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
                obj1.GetComponent<M_shop>().shopOpen += () =>
                {
                    Shopsshow = false;
                    HOW.SetActive(true);

                };
                Shopsshow = true;
                HOW.SetActive(false);
            }
            else
            {              
                Canvas.GetComponentInChildren<M_shop>()?.Close();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject.Find("Canvas").GetComponentInChildren<M_shop>()?.Close();
        HOW.SetActive(false);

    }
}
