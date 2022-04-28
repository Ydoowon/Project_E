using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PlayerShop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }   
    public void ISClose()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
    public void ISOpen()
    {
        Time.timeScale = 0;
        this.gameObject.SetActive(true);
    }
}
