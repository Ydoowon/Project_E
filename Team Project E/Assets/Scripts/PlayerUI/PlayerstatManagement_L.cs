using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerstatManagement_L : MonoBehaviour
{
    static public PlayerstatManagement_L instance;
    public GameObject myPlayer;
    public SGauge UnlockGauge;
    public SGauge StaminaGauge;
    //For HP
    public TMPro.TMP_Text playerHP;
    public Slider HPSlider;

    //For Hide
    public TMPro.TMP_Text playerHide;
    public Slider HideSlider;

    //For EX
    public TMPro.TMP_Text playerLevel;
    public TMPro.TMP_Text playerExp;
    public Slider EXSlider;

    CanvasGroup stamgauge;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        HPtext();
        Hidetext();
        LevelandExpText();
        stamgauge = StaminaGauge.gameObject.GetComponent<CanvasGroup>();
    }


    public void HPtext()
    {
        float HP = myPlayer.GetComponent<SPlayer>().MyStatus.HP;
        float MaxHP = myPlayer.GetComponent<SPlayer>().MyStatus.MaxHP;
        playerHP.text = (int)HP + " / " + (int)MaxHP;
        HPSlider.value = HP / MaxHP;
    }

    public void Hidetext()
    {
        float Hide = myPlayer.GetComponent<SPlayer>().MyStatus.Hidepoint;
        float MaxHide = myPlayer.GetComponent<SPlayer>().MyStatus.Max_hdPoint;
        playerHide.text = (int)Hide + " / " + MaxHide;
        HideSlider.value = Hide / MaxHide;
    }

    public void LevelandExpText()
    {
        int TextLevel = myPlayer.GetComponent<SPlayer>().MyStatus.PlayerLevel;
        playerLevel.text = "Lv. " + TextLevel.ToString("D2");

        float TextExp = myPlayer.GetComponent<SPlayer>().MyStatus.Exp;
        playerExp.text = (int)TextExp + " / " + TextLevel * TextLevel * 10;
        EXSlider.value = TextExp / (TextLevel * TextLevel * 10);
    }

    public void Unlocking(float point)
    {
        UnlockGauge.mySlider.value = (100.0f - point) / 100.0f;
        if (point <= 0)
        {
            UnlockSet(false);
        }
    }
    public void UnlockSet(bool boolean)
    {
        UnlockGauge.gameObject.SetActive(boolean);
    }

    public void Running(float point)
    {
        StaminaGauge.mySlider.value = point / 100.0f;
        if(StaminaGauge.mySlider.value < 0.98f)
        {
            StaminaGauge.gameObject.SetActive(true);
            stamgauge.alpha = 1;
        }
        else
        {
            stamgauge.alpha -= Time.fixedDeltaTime * 5.0f;
            if(stamgauge.alpha == 0)
            {
                StaminaGauge.gameObject.SetActive(false);
            }
        }
    }



}
