using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_playerShopOpen : MonoBehaviour
{
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
        How.text = "<#ff0000ff>E</color> <#000000ff>키를 진열대 열기";

    }
    private void OnTriggerExit(Collider other)
    {
        HOW.SetActive(false);

    }
}
