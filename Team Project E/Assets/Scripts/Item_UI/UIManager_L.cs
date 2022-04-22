using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager_L : MonoBehaviour
{
    public GameObject myInven;
    public GameObject myMap;
    RoomButton_L[] myButtons;


    Color CurColor;
    bool ActiveInven = false;
    bool ActiveMap = false;

    // Start is called before the first frame update
    void Start()
    {

        myInven.SetActive(ActiveInven);
        myMap.SetActive(ActiveMap);
        myButtons = myMap.GetComponentsInChildren<RoomButton_L>();
        foreach (RoomButton_L button in myButtons)
        {
            button.gameObject.SetActive(false);
        }

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
    

    public void SetMyButton(int Row, int Col)
    {
        if(myButtons != null)
        myButtons[(Row - 1) * 4 + Col - 1].gameObject.SetActive(true);

    }


}
