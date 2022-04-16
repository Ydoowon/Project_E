using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_shop : MonoBehaviour
{

    public GameObject layout2;
    // Start is called before the first frame update

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void BuyChekOpen()
    {
        this.GetComponent<M_Buymany>().layout += () => layout2.SetActive(false);
        layout2.SetActive(true);
        GameObject obj = Instantiate(Resources.Load("UI/M_Buymany"), this.transform) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

    }
    public void Close()
    {
        Destroy(this.gameObject);
    }
}
