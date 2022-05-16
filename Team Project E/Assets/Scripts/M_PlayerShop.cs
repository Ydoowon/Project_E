using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class M_PlayerShop : MonoBehaviour
{
    public event UnityAction Shopshow;
    public GameObject slae3;
    public GameObject slae4;
    public GameObject slae5;
    public GameObject slae6;
    public int tableMany = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*Table();
        if (Input.GetKeyDown(KeyCode.A))
        {
            tableMany += 1;        
        }*/
    }   
    public void Close()
    {
        Shopshow?.Invoke();
        this.gameObject.SetActive(false);
    }


    void Table()
    {
        if (tableMany >= 6)
        {
            tableMany = 6;
        }

        switch (tableMany)
        {
            case 6:
                slae6.SetActive(true);
                slae5.SetActive(true);
                slae4.SetActive(true);
                slae3.SetActive(true);
                break;
            case 5:
                slae5.SetActive(true);
                slae4.SetActive(true);
                slae3.SetActive(true);
                break;
            case 4:
                slae4.SetActive(true);
                slae3.SetActive(true);
                break;
            case 3:
                slae3.SetActive(true);
                break;
        }
    }



}
