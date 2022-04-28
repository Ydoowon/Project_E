using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SMonster : MonoBehaviour
{
    public STATE myState = STATE.NONE;
    public SAIperception myPerception;
    NavMeshAgent myNav;
    public LayerMask CrashMask;

    [SerializeField]
    float MoveSpeed = 3.0f;
    [SerializeField]
    float RunSpeed = 6.0f;

    public float Damage = 5.0f;
    float AttackDelay = 0.0f;
    [SerializeField]
    float MissingTime = 5.0f;
    
    public List<Transform> DestList = new List<Transform>();
    //순찰중 이동할 위치에 대한 리스트
    int ListCnt = 0;

    Animator _anim;

    Animator myAnim
    {
        get
        {
            if (_anim == null)
                _anim = GetComponentInChildren<Animator>();
            return _anim;
        }
    }
    SAnimEvent _animEvent;
    SAnimEvent myAnimEvent
    {
        get
        {
            if (_animEvent == null)
            {
                _animEvent = this.GetComponentInChildren<SAnimEvent>();
            }
            return _animEvent;
        }
    }


    public enum STATE
    {
        NONE, CREATE, IDLE, MOVE, FOLLOW, DEATH
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(STATE.CREATE);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    public void Attack()
    {
        if(myPerception.myEnemyList[0] !=null)
        {
            myPerception.myEnemyList[0].GetComponent<SPlayer>().Ondamage(Damage);
        }
    }
    public void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.CREATE:
                myNav = this.GetComponent<NavMeshAgent>();
                myAnimEvent.Attack += Attack;
                ChangeState(STATE.IDLE); // 생성후 Play STATE로 변경
                break;
            case STATE.IDLE:
                myAnim.SetBool("IsWalk", false);
                myAnim.SetBool("IsRun", false);
                myNav.ResetPath();
                StartCoroutine(Wait(3.0f));
                break;
            case STATE.MOVE:
                myNav.speed = MoveSpeed;
                myNav.stoppingDistance = 1.0f;
                myAnim.SetBool("IsWalk", true);
                myAnim.SetBool("IsRun", false);
                break;
            case STATE.FOLLOW:
                myNav.speed = RunSpeed;
                myNav.stoppingDistance = 5.0f;
                myAnim.SetBool("IsRun", true);
                break;
            case STATE.DEATH:
                break;
        }
    }

    public void StateProcess()
    {

        switch (myState)
        {
            case STATE.CREATE:
                break;
            case STATE.IDLE:
                if (myPerception.myEnemyList.Count > 0)
                    FindTarget(myPerception.myEnemyList[0]);
                break;
            case STATE.MOVE:
                if (myPerception.myEnemyList.Count > 0)
                    FindTarget(myPerception.myEnemyList[0]);
                
                MoveToDestination();
                break;
            case STATE.FOLLOW:
                if (myPerception.myEnemyList.Count == 0)
                {
                    myNav.SetDestination(this.gameObject.transform.position);
                    ChangeState(STATE.IDLE);
                }
                else 
                {
                    //플레이어 공격 & 추적
                    FindTarget(myPerception.myEnemyList[0]);
                    AttackRoutine();
                }
                break;
            case STATE.DEATH:
                break;
        }
    }
    public void AttackRoutine()
    {
        if ((transform.position - myPerception.myEnemyList[0].transform.position).magnitude <= myNav.stoppingDistance)
        {
            myAnim.SetBool("AttPossible", true);
            this.transform.LookAt(myPerception.myEnemyList[0].transform);
            //플레이어가 죽었을 때
            if (myPerception.myEnemyList[0].GetComponent<SPlayer>().myState == SPlayer.STATE.DEATH)
            {
                myAnim.SetBool("AttPossible", false);
                myPerception.myEnemyList.RemoveAt(0);
                ChangeState(myAnim.GetBool("IsWalk") ? STATE.MOVE : STATE.IDLE); // 이전 상태로 복귀
            }
            else
            {
                if (AttackDelay > 0.0f)
                {
                    AttackDelay -= Time.deltaTime;
                }
                else
                {
                    AttackDelay = 2.0f;
                    myAnim.SetTrigger("Attack");
                }
            }
        }
        else
        {
            myAnim.SetBool("AttPossible", false);
            AttackDelay = 0.0f;
            myNav.SetDestination(myPerception.myEnemyList[0].transform.position);
        }
    }



    void MoveToDestination() // 목적한 자리로 이동하는 함수
    {
        if (DestList.Count == 0) return;

        myNav.SetDestination(DestList[ListCnt].position);
        float Dist = (DestList[ListCnt].position - this.transform.position).magnitude;
        if(Dist <= myNav.stoppingDistance) // 목적지에 도착했을 경우
        {
            if (ListCnt < DestList.Count - 1)
            {
                ListCnt++;  // 다음 목적지로 
            }
            else if(ListCnt >= DestList.Count - 1)
            {
                ListCnt = 0; // 첫 목적지로 초기화
            }
            ChangeState(STATE.IDLE);
        }
    }


    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);

        if (myState == STATE.IDLE)
        {
            // Wait Coroutine이 실행되고 있는 동안 여전히 IDLE 상태라면 (=follow로 변경되지 않았다면)
            ChangeState(STATE.MOVE);
        }
    }


    public void FindTarget(GameObject Target)   // 플레이어를 찾아 다닌다.
    {
        if (Target == null) return;

        Vector3 pos = (Target.transform.position - this.transform.position).normalized;
        float Angle = Mathf.Acos(Vector3.Dot(this.transform.forward, pos)) * 180.0f / Mathf.PI;
        // 플레이어와 몬스터 사이 방향 벡터와 몬스터의 forward 백터사이의 각을 구함

        // 앵글이 -45 ~ 45도 사이일 때, 플레이어가 숨지않았을 때
        if (Angle < 45.0f && !Target.GetComponent<SPlayer>().OnHide)  
        {
            
            Ray ray = new Ray(this.transform.position + new Vector3(0,2,0), pos);  // 자신에서 플레이어로 향하는 Ray 생성
            if (Physics.Raycast(ray, out RaycastHit hit, 30.0f, CrashMask))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    MissingTime = 5.0f;
                    ChangeState(STATE.FOLLOW);
                }
            }
            else
            {
                Missing();
            }
        }
        else
        {
            Missing();
        }
        
    }
    public void Missing()
    {
        if (myState != STATE.FOLLOW) return;

        MissingTime -= Time.deltaTime;
        if (MissingTime <= 0.0f)
        {
            myNav.SetDestination(this.gameObject.transform.position);
            ChangeState(STATE.IDLE);
            MissingTime = 5.0f;
        }
    }

   
}

/*
public void FollowTarget()
{
    if(MoveRoutine != null) StopCoroutine(MoveRoutine);
    MoveRoutine = StartCoroutine(Following());

    if(RotRoutine != null) StopCoroutine(RotRoutine);
    RotRoutine = StartCoroutine(Rotating());

}


IEnumerator Following()
{
    while (myState == STATE.FOLLOW)  // FOLLOW 상태일 때 계속 진행
    {
        if (myPerception.myEnemyList.Count == 0) break;

        float delta = MoveSpeed * Time.deltaTime;
        this.transform.position += this.transform.forward * delta;
        yield return null;
    }

    MoveRoutine = null;
}*/
/*
 public void MoveAround(float Dist)   // 주위를 랜덤으로 배회한다
 {
     if (RotRoutine == null && MoveRoutine == null)
         RotRoutine = StartCoroutine(Rotating());

     if (MoveRoutine != null) return;
     MoveRoutine = StartCoroutine(Moving(Dist));
 }
*/
/*
IEnumerator Rotating()
{

    float RotAngle = Random.Range(0.0f, 180.0f);
    float Dir = 1.0f;
    if (RotAngle > 180.0f)
        Dir = -1.0f;

    while (RotAngle > 0)  //  돌아야 할 각도가 남아 있을 때
    {
        if (myState == STATE.FOLLOW) break;

        float delta = RotSpeed * Time.deltaTime;

        if (RotAngle < delta)
        {
            delta = RotAngle;
        }

        this.transform.Rotate(Vector3.up, delta * Dir, Space.World);
        RotAngle -= delta;

        yield return null;
    }
    RotRoutine = null;
}
*/
/*
IEnumerator Moving(float Dist)
{

    while (Dist > 0.0f)   //move상태일때 계속 와리가리 하면서 이동
    {
        if (myState == STATE.FOLLOW) break;

        float delta = MoveSpeed * Time.deltaTime;
        if (delta > Dist)
            delta = Dist;

        this.transform.position += this.transform.forward * delta;
        Dist -= delta;
        yield return null;
    }
    yield return StartCoroutine(Wait(3.0f));

    ChangeState(myAnim.GetBool("IsWalk") ? STATE.MOVE : STATE.IDLE);

    MoveRoutine = null;

    // 루틴이 끝나면 3초 대기
}
*/