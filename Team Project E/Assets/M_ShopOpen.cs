using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ShopOpen : MonoBehaviour
{
    public event UnityAction Shoping = null;
    public TMPro.TMP_Text How;
    public GameObject HOW;
    bool Open = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Canvas").GetComponent<M_ui>().Open += () => Open = !Open;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        HOW.SetActive(true);
        How.text = "<#ff0000ff>E</color> <#000000ff>키를 눌러 상점열기";
        Shoping?.Invoke();

    }
    private void OnTriggerExit(Collider other)
    {
        HOW.SetActive(false);
        Shoping?.Invoke();
        if (Open)
        {
            GameObject.Find("Canvas").GetComponentInChildren<M_shop>().Close();
        }

    }
}
