using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_playerShopOpen : MonoBehaviour
{
    public TMPro.TMP_Text How;
    public GameObject HOW;
    public event UnityAction PlayerShoprng;
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
        How.text = "<#ff0000ff>E</color> <#000000ff>Ű�� ������ ����";

    }
    private void OnTriggerExit(Collider other)
    {
        PlayerShoprng?.Invoke();
        HOW.SetActive(false);

    }
}