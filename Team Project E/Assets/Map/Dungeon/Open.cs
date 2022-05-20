using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    Animator myani;
    [SerializeField]
    SOrb.ColorType[] order;
    public List<SOrb> myOrblist;
    public int OrderCnt = 0;  // 주어진 컬러에 순서대로 눌렀나 체크한다
    [SerializeField]
    float Lockgauge = 100.0f;
    [SerializeField]
    bool orbLocked = true;
    public bool OrbLocked
    {
        get { return orbLocked; }
        set { orbLocked = value; }
    }
    public bool DoorOpen = false;

    void Start()
    {
        myani = this.GetComponent<Animator>();
    }

    public void SetOrbLock(SOrb orb)
    {
        myOrblist.Add(orb);
        if (myOrblist.Count == order.Length)
        {
            bool result = true;  // orblock 이 풀리게 할지에 대한 결과 값
            for (int i=0; i < order.Length; i++) // 플레이어가 추가한 리스트와 본인이 가진 정답 비교
            {
                if (myOrblist[i].myType != order[i])
                {
                    result = false; // 하나라도 틀리면 실패
                }
            }
            if(result == true)
            {
                OrbLocked = !result; // 결과값 반환
                myOrblist.Clear();
            }
            else
            {
                foreach(SOrb _orb in myOrblist)
                {
                    _orb.ResetOrb();
                }
                myOrblist.Clear();
            }
        }
    }

    public void DoorUnlock(float speed)
    {
        if (OrbLocked == true) return; // 오브 퍼즐이 풀리지 않으면 문이 열리지 않는다.


        Lockgauge -= Time.deltaTime * speed;
        if (Lockgauge <= 0)
        {
            Lockgauge = 0.0f;
            myani.SetTrigger("Open");
            DoorOpen = true;
            Invoke("Invisible", 3.0f);
        }
    }
    public float GetLockgauge()
    {
        return Lockgauge;
    }

    void Invisible()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!DoorOpen && other.gameObject.layer == LayerMask.NameToLayer("Player")) // 문이 열리지 않았을때만 작동
        {

            if(!OrbLocked)  // 퍼즐을 풀었을때
            {
                other.GetComponent<SPlayer>().Door = this;
                SGameManager.instance.Message.text = "키를 꾹 눌러 문 열기";
                SGameManager.instance.PressE.SetActive(true);
            }
            else  // 풀지 않고 문에 접근했을 때
            {
                SGameManager.instance.Message.text = "먼저 오브를 활성화 하십시오";
                SGameManager.instance.PressE.SetActive(true);
                SGameManager.instance.Interaction.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<SPlayer>().Orb = null;
            SGameManager.instance.Interaction.SetActive(true);
            SGameManager.instance.PressE.SetActive(false); // press E만 비활성화 됐을 경우도 있으므로 함께 처리
        }
    }
}
