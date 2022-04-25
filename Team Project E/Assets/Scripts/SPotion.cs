using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SPotion : SItem, IPointerUpHandler
{
    public float HP;
    public float Speed;
    public float HidePoint;
    [SerializeField]
    GameObject Player;
    
    public float ClickTime = 0.0f;
    // Update is called once per frame

    public void OnPointerUp(PointerEventData eventData)
    {
        if ((Time.time - ClickTime) < 0.3f)
        {
            OnMouseDoubleClick(Player);
            ClickTime = -1;
        }
        else
        {
            Debug.Log("DoubleClick");
            ClickTime = Time.time;
        }
    }

    void OnMouseDoubleClick(GameObject Target)
    {
        if (ItemData.ItemType != SItemData.Type.Consume) return;

        if(HP != 0) // 회복량이 0이 아니라면
        {
            float Target_HP = Target.GetComponent<SPlayer>().MyStatus.HP;
            if (Target_HP == 100) return;
            else
            {
                Target.GetComponent<SPlayer>().HealingHP(HP);
            }
        }
        if(HidePoint != 0) // 회복량이 0이 아니라면
        {

            float Target_HidePoint = Target.GetComponent<SPlayer>().MyStatus.Hidepoint;
            if (Target_HidePoint == 100) return;
            else
            {
                Target.GetComponent<SPlayer>().HealingHidePoint(HidePoint);
            }
        }

    }
}
