using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swarmp : MonoBehaviour
{
    public LayerMask CrashMask;
    public float Downspeed = 2.0f;   // 디폴트 다운 스피드는 2.0f
    public float Duration = 0.5f;    // 디폴트 지속시간은 0.5f초로 해놓음
    public float Damage = 5.0f;      // 디폴트 데미지는 5.0f로 해놓음
    
   

public void OnTriggerEnter(Collider other) //충돌시 
    {
   
        if ((CrashMask & (1 << other.gameObject.layer)) > 0)
        {
            SPlayer Target = other.GetComponent<SPlayer>();

            Target.SetSpeed(Downspeed, Duration);  // 플레이어에게 본인이 가지고 있는 Downspeed, Duration 값을 넘김

            if (Damage > 0)
                StartCoroutine(Poison()); //지속데미지
        }

    }
    

    public IEnumerator Poison()
    {
        int i = 0;
        while (true)
        {
            
            ++i; //데미지를 준 횟수로 체크
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
