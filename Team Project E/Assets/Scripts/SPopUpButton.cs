using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPopUpButton : MonoBehaviour
{
    public GameObject NoTouch;
    public TMPro.TMP_Text Map_Name;

    public void TimeStop(TMPro.TMP_Text Mapname)
    {
        NoTouch.SetActive(true);
        Map_Name.text = Mapname.text;
        Time.timeScale = 0.0f;
    }
    public void TimeStart()
    {
        this.gameObject.SetActive(false);
        NoTouch.SetActive(false);
        Time.timeScale = 1.0f;
    }

}
