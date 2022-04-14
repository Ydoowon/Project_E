using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SMonster : MonoBehaviour
{
    public STATE myState = STATE.NONE;
    Coroutine MoveRoutine = null;
    Coroutine RotRoutine = null;
    public SAIperception myPerception;
    NavMeshAgent myNav;
    public LayerMask CrashMask;

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

    public float MoveSpeed = 1.5f;
    public float RotSpeed = 360.0f;
    public float Damage = 5.0f;
    float AttackDelay = 0.0f;

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
                StartCoroutine(Wait(3.0f));
                break;
            case STATE.MOVE:
                myAnim.SetBool("IsWalk", true);
                break;
            case STATE.FOLLOW:
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
                float Dist = Random.Range(5.0f, 7.0f);
                if (myPerception.myEnemyList.Count > 0)
                    FindTarget(myPerception.myEnemyList[0]);
                MoveAround(Dist);
                break;
            case STATE.FOLLOW:
                if (myPerception.myEnemyList.Count == 0)
                {
                    myNav.isStopped = true;
                    myAnim.SetBool("IsRun", false);
                    ChangeState(STATE.MOVE);
                }
                else
                {
                    if ((transform.position - myPerception.myEnemyList[0].transform.position).magnitude <= myNav.stoppingDistance)
                    {
                        myAnim.SetBool("AttPossible", true);
                        this.transform.LookAt(myPerception.myEnemyList[0].transform);
                        //플레이어가 죽었을 때
                        if (myPerception.myEnemyList[0].GetComponent<SPlayer>().myState == SPlayer.STATE.DEATH)
                        {
                            myAnim.SetBool("AttPossible", false);
                            myAnim.SetBool("IsRun", false);
                            myNav.isStopped = true;
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
                                myNav.isStopped = true;
                                myAnim.SetTrigger("Attack");
                            }
                        }
                    }
                    else
                    {
                        myAnim.SetBool("AttPossible", false);
                        AttackDelay = 0.0f;
                        myNav.isStopped = false;
                        myNav.SetDestination(myPerception.myEnemyList[0].transform.position);
                    }
                }
                break;
            case STATE.DEATH:
                break;
        }
    }

    public void MoveAround(float Dist)   // 주위를 랜덤으로 배회한다
    {
        if (RotRoutine == null && MoveRoutine == null)
            RotRoutine = StartCoroutine(Rotating());

        if (MoveRoutine != null) return;
        MoveRoutine = StartCoroutine(Moving(Dist));
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
    IEnumerator Moving(float Dist)
    {
        // 회전 다할때까지 대기

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
        ChangeState(STATE.IDLE);
        MoveRoutine = null;

        // 루틴이 끝나면 3초 대기
    }
    public void FindTarget(GameObject Target)   // 플레이어를 찾아 다닌다.
    {
        if (Target == null) return;

        Vector3 pos = (Target.transform.position - this.transform.position).normalized;

        float Angle = Mathf.Acos(Vector3.Dot(this.transform.forward, pos)) * 180.0f / Mathf.PI;
        // 플레이어와 몬스터 사이 방향 벡터와 몬스터의 forward 백터사이의 각을 구함

       

       
        // 앵글이 -30 ~ 30도 사이일 때, 플레이어가 숨지않았을 때
        if (Angle < 30.0f && !Target.GetComponent<SPlayer>().OnHide)  
        {
            
            Ray ray = new Ray(this.transform.position, pos);  // 자신에서 플레이어로 향하는 Ray 생성
            if (Physics.Raycast(ray, 10.0f, CrashMask)) return;  // 벽에 가로막혀 있다면 return
            
            ChangeState(STATE.FOLLOW); // 아니라면 상태를 FOLLOW 상태로 변경
        }

    }

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