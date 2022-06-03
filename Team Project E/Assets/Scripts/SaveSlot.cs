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
            myPopUp.Name.text = (index + 1) + "�� ���Կ� �̹� ������ �ֽ��ϴ�.";
            myPopUp.Main.text = "������ ������ ����� <color=red>���̺�</color> �Ͻðڽ��ϱ�?";
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
            myPopUp.Name.text = (index + 1) + "�� ������ �ε��մϴ�.";
            myPopUp.Main.text = "������ �ε� �Ͻðڽ��ϱ�?";
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
