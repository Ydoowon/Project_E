using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class M_BuyChek : MonoBehaviour
{
    public event UnityAction Layout = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Buy()
    {
        Layout?.Invoke();
        Destroy(this.gameObject);

    }
    public void Cencel()
    {
        Layout?.Invoke();
        Destroy(this.gameObject);
    }
}
