using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager_L : MonoBehaviour
{
    public GameObject myInven;
    public GameObject myMap;


    Color CurColor;
    bool ActiveInven = false;
    bool ActiveMap = false;

    // Start is called before the first frame update
    void Start()
    {

        myInven.SetActive(ActiveInven);
        myMap.SetActive(ActiveMap);


    }
    // Update is called once per frame
    void Update()
    {
        InvenOnOff(); // Ieven On,Off
        MapOnOff();
        //Pause();      // Game Pause
    }
    void InvenOnOff()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActiveInven = !ActiveInven;
            myInven.SetActive(ActiveInven);
        }
    }

    void MapOnOff()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ActiveMap = !ActiveMap;
            myMap.SetActive(ActiveMap);
        }
    }
    /*
    void Pause()
    {
        if (ActiveInven)
        {
            Time.timeScale = 0;
            myLight.GetComponent<Light>().color = Color.black;
        }
        else
        {
            Time.timeScale = 1;
            myLight.GetComponent<Light>().color = CurColor;
        }
    }
    */
}
