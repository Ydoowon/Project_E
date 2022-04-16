using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_Buymany : MonoBehaviour
{
    public event UnityAction layout;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyCheckOpen()
    {
        GameObject obj = Instantiate(Resources.Load("UI/M_Buycheck"), this.transform) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
        Destroy(this.gameObject);

    }
    public void Cancel()
    {
        layout.Invoke();
        Destroy(this.gameObject);
    }
}
