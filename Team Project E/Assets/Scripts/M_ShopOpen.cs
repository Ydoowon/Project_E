using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_ShopOpen : MonoBehaviour
{
    public event UnityAction Shoping = null;
    public TMPro.TMP_Text How;
    public GameObject HOW;
    float hight = 580.0f;
    bool show = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(this.transform.position);
            pos.y = pos.y + hight;
            HOW.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        show = true;
        HOW.SetActive(true);
        How.text = "<#ff0000ff>E</color> <#000000ff>키를 눌러 상점열기";
        Shoping?.Invoke();

    }
    private void OnTriggerExit(Collider other)
    {
        show = false;
        HOW.SetActive(false);
        Shoping?.Invoke();
        GameObject.Find("Canvas").GetComponentInChildren<M_shop>()?.Close();


    }
}
