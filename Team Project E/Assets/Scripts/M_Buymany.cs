using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class M_Buymany : MonoBehaviour
{
    public event UnityAction layout = null;
    int many = 1;
    int MAX = 10;
    public TMPro.TMP_Text MANY;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Up()
    {
        if (many < MAX)
        {
            many += 1;
            MANY.text = "" + many;
        }
        else
        {
            many = 1;
            MANY.text = "" + many;
        }
    }
    public void Down()
    {
        if (many > 1)
        {
            many -= 1;
            MANY.text = "" + many;
        }
        else
        {
            many = MAX;
            MANY.text = "" + many;
        }
    }
    public void Buy()
    {
        GameObject obj = Instantiate(Resources.Load("UI/M_Check"), this.transform) as GameObject;
        obj.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);
        this.GetComponentInChildren<M_BuyChek>().Parent += () => Destroy(this.gameObject);
        this.GetComponentInChildren<M_BuyChek>().Layout += layout;

    }
    public void Cancel()
    {
        layout.Invoke();
        Destroy(this.gameObject);
    }
}
