using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_L : MonoBehaviour
{
    public GameObject myInven;
    public GameObject myLight;
    Color CurColor;
    bool ActiveInven = false;


    
    // Start is called before the first frame update
    void Start()
    {
        CurColor = myLight.GetComponent<Light>().color;
        myInven.SetActive(ActiveInven);

    }
    // Update is called once per frame
    void Update()
    {
        InvenOnOff(); // Ieven On,Off
        Pause();      // Game Pause
    }

    void InvenOnOff()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActiveInven = !ActiveInven;
            myInven.SetActive(ActiveInven);
        }
    }

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

    //public void AddItemDB(GameObject _gameobject)
    //{
    //    if (ItemDB.Count == 15) return;
    //    else
    //    {
    //        ItemDB.Add(_gameobject);
    //    }
    //}
}
