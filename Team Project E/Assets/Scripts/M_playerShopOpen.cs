using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_playerShopOpen : MonoBehaviour
{
    public TMPro.TMP_Text How;
    public GameObject HOW;
    public event UnityAction PlayerShoprng;
    float hight = 400.0f;
    bool show = false;
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
        show = true;
        PlayerShoprng?.Invoke();
        HOW.SetActive(true);
        How.text = "������ ����";

    }
    private void OnTriggerExit(Collider other)
    {
        show = false;
        PlayerShoprng?.Invoke();
        HOW.SetActive(false);

    }
}
