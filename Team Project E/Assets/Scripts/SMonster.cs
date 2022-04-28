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
    //������ �̵��� ��ġ�� ���� ����Ʈ
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
                ChangeState(STATE.IDLE); // ������ Play STATE�� ����
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
                    //�÷��̾� ���� & ����
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
            //�÷��̾ �׾��� ��
            if (myPerception.myEnemyList[0].GetComponent<SPlayer>().myState == SPlayer.STATE.DEATH)
            {
                myAnim.SetBool("AttPossible", false);
                myPerception.myEnemyList.RemoveAt(0);
                ChangeState(myAnim.GetBool("IsWalk") ? STATE.MOVE : STATE.IDLE); // ���� ���·� ����
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



    void MoveToDestination() // ������ �ڸ��� �̵��ϴ� �Լ�
    {
        if (DestList.Count == 0) return;

        myNav.SetDestination(DestList[ListCnt].position);
        float Dist = (DestList[ListCnt].position - this.transform.position).magnitude;
        if(Dist <= myNav.stoppingDistance) // �������� �������� ���
        {
            if (ListCnt < DestList.Count - 1)
            {
                ListCnt++;  // ���� �������� 
            }
            else if(ListCnt >= DestList.Count - 1)
            {
                ListCnt = 0; // ù �������� �ʱ�ȭ
            }
            ChangeState(STATE.IDLE);
        }
    }


    IEnumerator Wait(float t)
    {
        yield return new WaitForSeconds(t);

        if (myState == STATE.IDLE)
        {
            // Wait Coroutine�� ����ǰ� �ִ� ���� ������ IDLE ���¶�� (=follow�� ������� �ʾҴٸ�)
            ChangeState(STATE.MOVE);
        }
    }


    public void FindTarget(GameObject Target)   // �÷��̾ ã�� �ٴѴ�.
    {
        if (Target == null) return;

        Vector3 pos = (Target.transform.position - this.transform.position).normalized;
        float Angle = Mathf.Acos(Vector3.Dot(this.transform.forward, pos)) * 180.0f / Mathf.PI;
        // �÷��̾�� ���� ���� ���� ���Ϳ� ������ forward ���ͻ����� ���� ����

        // �ޱ��� -45 ~ 45�� ������ ��, �÷��̾ �����ʾ��� ��
        if (Angle < 45.0f && !Target.GetComponent<SPlayer>().OnHide)  
        {
            
            Ray ray = new Ray(this.transform.position + new Vector3(0,2,0), pos);  // �ڽſ��� �÷��̾�� ���ϴ� Ray ����
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
    while (myState == STATE.FOLLOW)  // FOLLOW ������ �� ��� ����
    {
        if (myPerception.myEnemyList.Count == 0) break;

        float delta = MoveSpeed * Time.deltaTime;
        this.transform.position += this.transform.forward * delta;
        yield return null;
    }

    MoveRoutine = null;
}*/
/*
 public void MoveAround(float Dist)   // ������ �������� ��ȸ�Ѵ�
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

    while (RotAngle > 0)  //  ���ƾ� �� ������ ���� ���� ��
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

    while (Dist > 0.0f)   //move�����϶� ��� �͸����� �ϸ鼭 �̵�
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

    // ��ƾ�� ������ 3�� ���
}
*/