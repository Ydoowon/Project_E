using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_playerShopOpen : MonoBehaviour
{
    public TMPro.TMP_Text How;
    public GameObject HOW;
    public event UnityAction PlayerShoprng;
<<<<<<< HEAD
=======
    float hight = 400.0f;
    bool show = false;
>>>>>>> parent of e1525bbf (.)
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
        PlayerShoprng?.Invoke();
        HOW.SetActive(true);
        How.text = "<#ff0000ff>E</color> <#000000ff>키를 진열대 열기";

    }
    private void OnTriggerExit(Collider other)
    {
        show = false;
        PlayerShoprng?.Invoke();
        HOW.SetActive(false);

    }
}
