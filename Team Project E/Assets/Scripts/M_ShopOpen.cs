using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ShopOpen : MonoBehaviour
{
    public event UnityAction Shoprng = null;
    public TMPro.TMP_Text How; // ?????? ????
    public GameObject HOW; // ?????? ????
    public GameObject UI; // ???????? ???? UI????????
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (UI.GetComponent<M_ui>().Shopshow == true) HOW.SetActive(true);
        else HOW.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        How.text = "<#ff0000ff>E</color> <#000000ff>?? ?? ????";
        Shoprng?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        HOW.SetActive(false);
        Shoprng?.Invoke();
        UI.GetComponentInChildren<M_shop>()?.Close();


    }
}
