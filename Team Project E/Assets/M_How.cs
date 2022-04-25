using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_How : MonoBehaviour
{
    public GameObject position;
    float hight = 580;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(position.transform.position);
        pos.y = pos.y + hight;
        this.GetComponent<RectTransform>().anchoredPosition = pos;
    }
}
