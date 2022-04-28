using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_menu : MonoBehaviour
{
    public GameObject layout;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Open()
    {
        Time.timeScale = 0;
        layout.SetActive(true);
           
    }
    public void Close()
    {
        Time.timeScale = 1;
        layout.SetActive(false);
        Destroy(this.gameObject);
    }
}
