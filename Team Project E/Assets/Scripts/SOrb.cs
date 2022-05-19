using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOrb : MonoBehaviour
{
    MeshRenderer myMeshRenderer;
    bool IsEnabled = false;   // 활성화 되면 선명하게, 비활성화 되면 투명하게 보이도록 한다.

    [SerializeField]
    Open myDoor; // 오브와 연결된 문을 설정
    public enum ColorType
    {
        RED,GREEN,BLUE
    }

    
    public ColorType myType;

    void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        myMeshRenderer.material = Instantiate(myMeshRenderer.material);
        switch (myType)
        {
            case ColorType.RED:
                myMeshRenderer.material.color = new Color(1, 0, 0, 0.3f);
                break;
            case ColorType.GREEN:
                myMeshRenderer.material.color = new Color(0, 1, 0, 0.3f);
                break;
            case ColorType.BLUE:
                myMeshRenderer.material.color = new Color(0, 0, 1, 0.3f);
                break;
        }
    }

    public void SetAlpha()
    {
        if (myDoor.OrbLocked == false) return;// 문이 잠겨있을 때만 작동한다

        IsEnabled = !IsEnabled;
        Color SetColor = myMeshRenderer.material.color;
        SetColor.a = IsEnabled ? 1.0f : 0.3f;
        myMeshRenderer.material.color = SetColor;
        SGameManager.instance.Message.text = IsEnabled? "E키를 눌러 비활성화" : "E키를 눌러 활성화";

        if (IsEnabled)
        {
            myDoor.SetOrbLock(this);
        }
        else
        {
            myDoor.myOrblist.Remove(this);
        }
    }
    public void ResetOrb()
    {
        IsEnabled = false;
        Color SetColor = myMeshRenderer.material.color;
        SetColor.a = 0.3f;
        myMeshRenderer.material.color = SetColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<SPlayer>().Orb = this;
            SGameManager.instance.Message.text = IsEnabled ? "E키를 눌러 비활성화" : "E키를 눌러 활성화";
            SGameManager.instance.PressE.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<SPlayer>().Orb = null;
            SGameManager.instance.PressE.SetActive(false);
        }
    }
}
