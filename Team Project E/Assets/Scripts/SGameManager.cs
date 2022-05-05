using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SGameManager : MonoBehaviour
{
    static public SGameManager instance;

    public Animator FadeAnim;
    public GameObject ItemToolTip;
    public SItem[] Itemlist = null;
    public GameObject PressE;
    public GameObject Interaction;
    public TMPro.TMP_Text Message;
    public GameObject DungeonSelectUI;

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

}
