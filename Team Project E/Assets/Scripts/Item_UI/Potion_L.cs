using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Potion_L : MonoBehaviour , IPointerUpHandler
{
    float clickTime = 0;
    GameObject myPlayer = null;
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDoubleClick()
    {
        float HP = myPlayer.GetComponent<SPlayer>().Hp;
        if (HP == 100) return;
        else
        {
            if (HP > 80)
            {
                myPlayer.GetComponent<SPlayer>().Ondamage(-(100-HP));
            }
            else
            {
                myPlayer.GetComponent<SPlayer>().Ondamage(-20);
            }
        }
        Destroy(gameObject);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if ((Time.time - clickTime) < 0.3f)
        {
            OnMouseDoubleClick();
            clickTime = -1;
        }
        else
        {
            clickTime = Time.time;
        }
    }
}
