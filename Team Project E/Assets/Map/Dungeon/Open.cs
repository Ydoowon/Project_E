using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    Animator myani;
    [SerializeField]
    SOrb.ColorType[] order;
    public List<SOrb> myOrblist;
    public int OrderCnt = 0;  // �־��� �÷��� ������� ������ üũ�Ѵ�
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
            bool result = true;  // orblock �� Ǯ���� ������ ���� ��� ��
            for (int i=0; i < order.Length; i++) // �÷��̾ �߰��� ����Ʈ�� ������ ���� ���� ��
            {
                if (myOrblist[i].myType != order[i])
                {
                    result = false; // �ϳ��� Ʋ���� ����
                }
            }
            if(result == true)
            {
                OrbLocked = !result; // ����� ��ȯ
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
        if (OrbLocked == true) return; // ���� ������ Ǯ���� ������ ���� ������ �ʴ´�.


        Lockgauge -= Time.deltaTime * speed;
        if (Lockgauge <= 0)
        {
            Lockgauge = 0.0f;
            myani.SetTrigger("Open");
            DoorOpen = true;
        }
    }
    public float GetLockgauge()
    {
        return Lockgauge;
    }

}
