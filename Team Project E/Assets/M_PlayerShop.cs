using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class M_PlayerShop : MonoBehaviour
{
    public GameObject ReturnBT1;
    public GameObject ReturnBT2;
    public GameObject ReturnBT3;
    public GameObject ReturnBT4;
    public GameObject ReturnBT5;
    public GameObject ReturnBT6;
    public GameObject ReturnBT7;
    public GameObject ReturnBT8;

    public GameObject slae3;
    public GameObject slae4;
    public GameObject slae5;
    public GameObject slae6;
    public GameObject slae7;
    public GameObject slae8;
    public static int tableMany = 2;

    public int gold = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Table();
        if (Input.GetKeyDown(KeyCode.A))
        {
            tableMany += 1;        
        }
    }   
    public void Close()
    {
        Destroy(this.gameObject);
    }

    public void Return()
    {
       

    }
    public void ResetGold()
    {

    }

    void Table()
    {
        if (tableMany >= 8)
        {
            tableMany = 8;
        }

        switch (tableMany)
        {
            case 8:
                slae8.SetActive(true);
                slae7.SetActive(true);
                slae6.SetActive(true);
                slae5.SetActive(true);
                slae4.SetActive(true);
                slae3.SetActive(true);
                break;
            case 7:
                slae7.SetActive(true);
                slae6.SetActive(true);
                slae5.SetActive(true);
                slae4.SetActive(true);
                slae3.SetActive(true);
                break;
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