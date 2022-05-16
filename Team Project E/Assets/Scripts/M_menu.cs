using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class M_menu : MonoBehaviour
{
    public GameObject layout;
    public event UnityAction MenuShow = null;

    bool IsGameSaveSlot = false;
    public GameObject GameSaveSlot;

    bool IsGameLoadSlot = false;
    public GameObject GameLoadSlot;

    bool IsGameEtcSlot = false;
    public GameObject GameEtcSlot;

    bool IsGameTip = false;
    public GameObject GameEtcTip;

    public GameObject MenuSelectText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Open()
    {
        Time.timeScale = 0;
        layout.SetActive(true);

    }
    public void Close()
    {
        Time.timeScale = 1;
        MenuShow?.Invoke();
        layout.SetActive(false);
        Destroy(this.gameObject);
    }

    public void OnOffGameSave()
    {
        if (IsGameLoadSlot)
        {
            GameLoadSlot.SetActive(false);
            IsGameLoadSlot = false;
        }

        if (IsGameEtcSlot)
        {
            GameEtcSlot.SetActive(false);
            IsGameEtcSlot = false;
        }

        if (IsGameTip)
        {
            GameEtcTip.SetActive(false);
            IsGameTip = false;
        }

        IsGameSaveSlot = !IsGameSaveSlot;
        GameSaveSlot.SetActive(IsGameSaveSlot);
        OnOffSelectMeue();
    }

    public void OnOffGameLoad()
    {
        if (IsGameSaveSlot)
        {
            GameSaveSlot.SetActive(false);
            IsGameSaveSlot = false;
        }

        if (IsGameEtcSlot)
        {
            GameEtcSlot.SetActive(false);
            IsGameEtcSlot = false;
        }

        if (IsGameTip)
        {
            GameEtcTip.SetActive(false);
            IsGameTip = false;
        }
        IsGameLoadSlot = !IsGameLoadSlot;
        GameLoadSlot.SetActive(IsGameLoadSlot);
        OnOffSelectMeue();
    }

    public void OnOffGameEtc()
    {
        if (IsGameSaveSlot)
        {
            GameSaveSlot.SetActive(false);
            IsGameSaveSlot = false;
        }

        if (IsGameLoadSlot)
        {
            GameLoadSlot.SetActive(false);
            IsGameLoadSlot = false;
        }

        if (IsGameTip)
        {
            GameEtcTip.SetActive(false);
            IsGameTip = false;
        }
        IsGameEtcSlot = !IsGameEtcSlot;
        GameEtcSlot.SetActive(IsGameEtcSlot);
        OnOffSelectMeue();
    }

    public void OnOffGameTip()
    {
        if (IsGameSaveSlot)
        {
            GameSaveSlot.SetActive(false);
            IsGameSaveSlot = false;
        }

        if (IsGameEtcSlot)
        {
            GameEtcSlot.SetActive(false);
            IsGameEtcSlot = false;
        }

        if (IsGameLoadSlot)
        {
            GameLoadSlot.SetActive(false);
            IsGameLoadSlot = false;
        }
        IsGameTip = !IsGameTip;
        GameEtcTip.SetActive(IsGameTip);
        OnOffSelectMeue();
    }

    public void OnOffSelectMeue()
    {
        if (!IsGameSaveSlot && !IsGameLoadSlot && !IsGameEtcSlot && !IsGameTip)
        {
            MenuSelectText.SetActive(true);
        }
        else
        {
            MenuSelectText.SetActive(false);

        }
    }

    public void EndGameButton()
    {
        SceneLoader_L.Inst.LoadScene(0);
    }

}
