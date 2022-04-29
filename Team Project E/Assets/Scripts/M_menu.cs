using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_menu : MonoBehaviour
{
    public GameObject layout;
    public event UnityAction MenuShow = null;
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
        MenuShow?.Invoke();
        layout.SetActive(false);
        Destroy(this.gameObject);
    }
}
