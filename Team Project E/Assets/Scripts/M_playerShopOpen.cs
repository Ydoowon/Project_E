using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_playerShopOpen : MonoBehaviour
{
    public TMPro.TMP_Text How;
    public GameObject HOW;
    public GameObject Shop;
    bool PlayerShopshow = false;
    // Start is called before the first frame update
    void Start()
    {
        Shop.GetComponent<M_PlayerShop>().Shophow += () =>
        {
            PlayerShopshow = false;
            HOW.SetActive(true);

        };
    }
    private void OnTriggerEnter(Collider other)
    {
        HOW.SetActive(true);
        How.text = "진열대 열기";

    }
    private void OnTriggerStay(Collider other)
    {

        if (Input.GetKeyDown(KeyCode.E))
        {

            if (!PlayerShopshow)
            {
                Shop.SetActive(true);
                PlayerShopshow = true;
                HOW.SetActive(false);

            }
            else
            {
                Shop.GetComponent<M_PlayerShop>().Close();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

        Shop.GetComponent<M_PlayerShop>().Close();
        HOW.SetActive(false);

    }
}
