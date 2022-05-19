using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SPotion : SItem
{
    public float HP;
    public float Speed;
    public float HidePoint;
    
    public GameObject Player;
    
    public float ClickTime = 0.0f;
    // Update is called once per frame

    public override bool UsingItem(GameObject Target)
    {
        bool isEffected = false;

        if (HP != 0) // 회복량이 0이 아니라면
        {
            float Target_HP = Target.GetComponent<SPlayer>().MyStatus.HP;
            if (Target_HP != 100.0f)
            {
                Target.GetComponent<SPlayer>().HealingHP(HP);
                isEffected = true;
            }
        }
        if (HidePoint != 0) // 회복량이 0이 아니라면
        {

            float Target_HidePoint = Target.GetComponent<SPlayer>().MyStatus.Hidepoint;
            if (Target_HidePoint != 100.0f)
            {
                Target.GetComponent<SPlayer>().HealingHidePoint(HidePoint);
                isEffected = true;
            }
        }
        return isEffected;
    }



}
