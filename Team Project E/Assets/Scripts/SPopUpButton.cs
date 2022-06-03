using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SPopUpButton : MonoBehaviour
{
    public GameObject NoTouch;
    public TMPro.TMP_Text Name;
    public TMPro.TMP_Text Main;
    int MapNum;
    int SaveLoadNum;
    public UnityAction<int> SaveLoad = null;
    public UnityAction Move = null;

    public void TimeStop(TMPro.TMP_Text Mapname)
    {
        NoTouch.SetActive(true);
        Name.text = Mapname.text;
        Time.timeScale = 0.0f;
    }
    public void TimeStart()
    {
        SGameManager.instance.CreateNewMap(MapNum);
        this.gameObject.SetActive(false);
        NoTouch.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void SetMapNum(int num)
    {
        MapNum = num;
    }
    public void SetSaveLoadNum(int num)
    {
        SaveLoadNum = num;
    }

    public void Check()
    {
        SaveLoad?.Invoke(SaveLoadNum);
        SGameManager.instance.MapUI.PopUpList.Pop();
    }
    public void Cancel()
    {
        SaveLoad = null;
        SGameManager.instance.MapUI.PopUpList.Pop();
    }
    public void MoveCheck()
    {
        Move?.Invoke();
    }
    public void MoveCancel()
    {
        Move = null;
    }
}
