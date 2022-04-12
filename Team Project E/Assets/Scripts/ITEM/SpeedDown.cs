using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDown : MonoBehaviour
{


    public LayerMask CrashMask;
    public float Downspeed = 2.0f;   // ����Ʈ �ٿ� ���ǵ�� 2.0f
    public float Duration = 0.5f;    // ����Ʈ ���ӽð��� 0.5f�ʷ� �س���
    public float Damage = 5.0f;      // ����Ʈ �������� 5.0f�� �س���


    public void OnTriggerEnter(Collider other) //�浹�� 
    {

        if ((CrashMask & (1 << other.gameObject.layer)) > 0)
        {
            SPlayer Target = other.GetComponent<SPlayer>();

            Target.SetSpeed(Downspeed, Duration);  // �÷��̾�� ������ ������ �ִ� Downspeed, Duration ���� �ѱ�

            if (Damage > 0)
                Target.Ondamage(Damage);

            Destroy(this.gameObject);
        }

    }
    /*
    IEnumerator DownSpeed(float t) // t=�ӵ������� ���ӽð� 
    {
        SPlayer MSpeed = GameObject.Find("Player").GetComponent<SPlayer>(); //�÷��̾� ��ũ��Ʈ ����

        MSpeed.MoveSpeed = 0.5f;

        yield return new WaitForSeconds(t); 

        MSpeed.MoveSpeed = 5f;

        Destroy(this.gameObject);
    }
    */
}