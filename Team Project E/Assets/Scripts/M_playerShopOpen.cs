using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_playerShopOpen : MonoBehaviour
{
    public TMPro.TMP_Text How;
    public GameObject HOW;
    public event UnityAction Onrange = null; // 범위의 bool값을 전달
    public GameObject Shop;
    // Start is called before the first frame update
    void Start()
    {
        Shop.GetComponent<M_PlayerShop>().Shophow += () => HOW.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        Onrange?.Invoke();
        HOW.SetActive(true);
        How.text = "진열대 열기";

    }
    private void OnTriggerExit(Collider other)
    {
        Onrange?.Invoke();
        Shop.GetComponent<M_PlayerShop>().Close();
        HOW.SetActive(false);

    }
}
