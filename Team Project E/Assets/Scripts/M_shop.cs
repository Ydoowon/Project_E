using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_shop : MonoBehaviour
{

    public GameObject layout;
    public event UnityAction shopOpen;
    public event UnityAction Open;
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BuyManyOpen()
    {
        GameObject obj = Instantiate(Resources.Load("UI/M_Buymany"), this.transform.parent) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
        obj.GetComponent<M_Buymany>().layout += () => layout.SetActive(false);
        layout.SetActive(true);       
    }
    public void Close()
    {
        shopOpen?.Invoke();
        Open?.Invoke();
        Destroy(this.gameObject);
    }
}
