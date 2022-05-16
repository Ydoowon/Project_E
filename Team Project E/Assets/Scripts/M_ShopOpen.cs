using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ShopOpen : MonoBehaviour
{
    public event UnityAction Shoping = null;
    public TMPro.TMP_Text How;
    public GameObject HOW;
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
        Shoping?.Invoke();

    }
    private void OnTriggerExit(Collider other)
    {
        HOW.SetActive(false);
        Shoping?.Invoke();
        GameObject.Find("Canvas").GetComponentInChildren<M_shop>()?.Close();


    }
}
