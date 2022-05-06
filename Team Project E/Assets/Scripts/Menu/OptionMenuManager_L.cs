using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenuManager_L : MonoBehaviour
{
    public List<GameObject> VolBar = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolUp()
    {
        //볼륨 (100/6)% 업
        //VolBar 증가
        for(int i = 0; i<6;i++)
        {
            if (VolBar[5].activeSelf == true) return;
            if(VolBar[i].activeSelf == false)
            {
                VolBar[i].SetActive(true);
                return;
            }
        }
    }

    public void VolDown()
    {
        //볼륨 (100/6)% Down
        //VolBar 감소
        for (int i = 5; i >= 0; i--)
        {
            if (VolBar[0].activeSelf == false) return;
            if (VolBar[i].activeSelf == true)
            {
                VolBar[i].SetActive(false);
                return;
            }
        }
    }
}
