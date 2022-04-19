using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmp : MonoBehaviour
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
                StartCoroutine(Poison()); //���ӵ�����
        }

    }
    

    public IEnumerator Poison()
    {
        int i = 0;
        while (true)
        {
            
            ++i; //�������� �� Ƚ���� üũ
            if(i >=5)
            {
              
                StopAllCoroutines();
            }

            SPlayer poison = GameObject.Find("Player").GetComponent<SPlayer>();
            poison.Ondamage(Damage );
            yield return new WaitForSeconds(5.0f);
}
        
       
    }
  
}
