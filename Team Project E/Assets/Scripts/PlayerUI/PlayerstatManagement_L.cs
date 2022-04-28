using UnityEngine;
using UnityEngine.UI;

public class PlayerstatManagement_L : MonoBehaviour
{
    static public PlayerstatManagement_L instance;
    public GameObject myPlayer;
    public GameObject UnlockGauge;

    //For HP
    public TMPro.TMP_Text playerHP;
    public Slider HPSlider;

    //For Hide
    public TMPro.TMP_Text playerHide;
    public Slider HideSlider;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HPtext();
        Hidetext();
    }

    public void HPtext()
    {
        float TextHP = myPlayer.GetComponent<SPlayer>().MyStatus.HP;
        playerHP.text = TextHP + " / " + "100";
        HPSlider.value = TextHP / 100;

    }

    public void Hidetext()
    {
        float TextHide = myPlayer.GetComponent<SPlayer>().MyStatus.Hidepoint;
        playerHide.text = (int)TextHide + " / " + "100";
        HideSlider.value = TextHide / 100;

    }

    public void Unlocking(float point)
    {
        UnlockGauge.GetComponent<SGauge>().mySlider.value = (100.0f - point) / 100.0f;
        if(point <= 0)
        {
            UnlockSet(false);
        }
    }
    public void UnlockSet(bool boolean)
    {
        UnlockGauge.gameObject.SetActive(boolean);
    }

}
