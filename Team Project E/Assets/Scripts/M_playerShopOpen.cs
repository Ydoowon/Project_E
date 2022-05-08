using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_playerShopOpen : MonoBehaviour
{
    public TMPro.TMP_Text How;
    public GameObject HOW;
    public event UnityAction PlayerShoprng;
    public GameObject Shop;
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
        PlayerShoprng?.Invoke();
        HOW.SetActive(true);
        How.text = "진열대 열기";

    }
    private void OnTriggerExit(Collider other)
    {
        PlayerShoprng?.Invoke();
        HOW.SetActive(false);
        Shop.GetComponent<M_PlayerShop>().Close();

    }
}
