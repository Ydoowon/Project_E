using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public bool isSaved = false;
    public TMP_Text Date;
    public TMP_Text Status;

    public GameObject noData;
    public GameObject SaveDataImage;
    public SPopUpButton myPopUp;
    public void Saving(int index)
    {
        if (!isSaved)
        {
            Save(index);
        }
        else
        {
            myPopUp.SaveLoad = Save;
            myPopUp.SetSaveLoadNum(index);
            myPopUp.Name.text = (index + 1) + "번 슬롯에 이미 파일이 있습니다.";
            myPopUp.Main.text = "기존의 파일을 지우고 <color=red>세이브</color> 하시겠습니까?";
            myPopUp.gameObject.SetActive(true);
            SGameManager.instance.MapUI.PushTolist(myPopUp.gameObject);
        }
    }

    public void Loading(int index)
    {
        if (!isSaved)
        {
            Load(index);
        }
        else
        {
            myPopUp.SaveLoad = Load;
            myPopUp.SetSaveLoadNum(index);
            myPopUp.Name.text = (index + 1) + "번 파일을 로드합니다.";
            myPopUp.Main.text = "정말로 로드 하시겠습니까?";
            myPopUp.gameObject.SetActive(true);
            SGameManager.instance.MapUI.PushTolist(myPopUp.gameObject);
        }
    }

    public void Save(int index)
    {
        SGameManager.instance.Save(index);
        noData.SetActive(false);
        SaveDataImage.SetActive(true);
    }

    public void Load(int index)
    {
        SGameManager.instance.Load(index);
        noData.SetActive(false);
        SaveDataImage.SetActive(true);
    }

}
