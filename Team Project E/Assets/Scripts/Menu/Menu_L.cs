using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_L : MonoBehaviour
{
    public GameObject FirstMenu;
    //public GameObject OptionMenu;

    // Start is called before the first frame update
    void Start()
    {
        GameObject odj = Instantiate(Resources.Load("UI/Canvas_FirstMenu")) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //public void CreateOptionMenu()
    //{
    //    GameObject odj = Instantiate(Resources.Load("UI/Canvas_OptionMenu")) as GameObject;
    //    Destroy(FirstMenu);
    //}

    //public void CreateMainMenu()
    //{
    //    GameObject odj = Instantiate(Resources.Load("UI/Canvas_FirstMenu")) as GameObject;
    //    Destroy(OptionMenu);
    //}


}
