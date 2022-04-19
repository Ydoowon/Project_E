using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SPlayer : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    float OriginMoveSpeed = 10.0f;

    float hAxis;
    float vAxis;
    bool Running;
    Coroutine SpeedSet;

    public float Hp = 100.0f;  // player HP
    public float HidePoint = 100.0f; // player 숨을수 있는 시간
    public bool OnHide = false;
    bool Down = false;

    public Transform myPlayer;
    public STATE myState = STATE.NONE;
    public LOCATION myLocation = LOCATION.TOWN;

    public Transform mySpringArm;
    public LayerMask InterMask;
    SStock_Shelves myStock;
    public GameObject MyMap;  // 인벤토릳 대신 임시로 넣어놓는 아이템
    Animator _Anim = null;
    Animator myAnim
    {
        get
        {
            if (_Anim == null) _Anim = GetComponentInChildren<Animator>();
            return _Anim;
        }
    }
    SAnimEvent _animEvent = null;
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
        NONE, CREATE, PLAY, DEATH
    }
    public enum LOCATION
    {
        TOWN, DUNGEON
    }

    void Start()
    {
        ChangeState(STATE.CREATE);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();

    }


    public void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.CREATE:
                myAnimEvent.StandUp += () =>
                {
                    Down = false;
                    OnHide = false;
                };// 숨은 상태 해제되도록 하는 delegate 전달
                ChangeState(STATE.PLAY); // 생성후 Play STATE로 변경
                break;
            case STATE.PLAY:
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
            case STATE.PLAY:

                if (!Down)  // 엎드린 상태가 아닐때만 이동 가능
                {
                    hAxis = Input.GetAxis("Horizontal");
                    vAxis = Input.GetAxis("Vertical");
                    Running = Input.GetButton("Run");
                  
                    Vector3 pos = new Vector3(hAxis, 0, vAxis).normalized;
                    Vector3 CompVec = Quaternion.AngleAxis(mySpringArm.rotation.eulerAngles.y, Vector3.up) * pos;

                    Moving(CompVec);
                }

                if (Input.GetKeyDown(KeyCode.Space) && HidePoint > 5.0f)
                    Hiding();

                HideSystem();
                break;
            case STATE.DEATH:
                break;
        }
    }
    public void Moving(Vector3 pos)
    {

        myAnim.SetBool("IsWalk", pos != Vector3.zero);
        switch(myLocation)
        {
            case LOCATION.TOWN:
            myAnim.SetBool("IsRun_T", Running);
                break;
            case LOCATION.DUNGEON:
            myAnim.SetBool("IsRun", Running);
                break;
        }
        
        myPlayer.LookAt(myPlayer.transform.position + pos);

        if (SpeedSet == null) // 함정에 걸리지 않은 상태에서만 작동
        {
            MoveSpeed = myAnim.GetBool("IsRun") || myAnim.GetBool("IsRun_T") ? OriginMoveSpeed : OriginMoveSpeed / 2;  //Run 상태면 5.0f, 아니면 절반
        }

        this.transform.Translate(pos * MoveSpeed * Time.deltaTime); // 이동  
    }

    public void Hiding()
    {
        if (Down == false)  // 숨지 않은 경우 숨는다
        {
            myAnim.SetTrigger("Hiding");  // Hiding 애니메이션 실행
            Down = true;
            OnHide = true;
        }
        else
        {
            myAnim.SetTrigger("StandUp");
        }

    }

    void HideSystem()
    {
        if (Down)
        {
            HidePoint -= Time.deltaTime * 5.0f;
        }

        if (HidePoint <= 0 && Down)  // Hidepoint가 0이고, 숨은 상태일 때
        {
            myAnim.SetTrigger("StandUp"); // 게이지 없으니 일어나게 만듬
        }

        if (!Down && HidePoint < 100.0f)
        {
            HidePoint += Time.deltaTime; // 숨은 상태가 아니라면 hidepoint 최대치까지 회복

        }
        HidePoint = Mathf.Clamp(HidePoint, 0.0f, 100.0f);
    }

    IEnumerator SpeedDown(float speed, float time)
    {
        if (MoveSpeed > speed)  // 가장 강력한 디버프를 받을 수 있도록 하기 위한 조건
        {
            MoveSpeed = speed;
        }
        yield return new WaitForSeconds(time);
        MoveSpeed = OriginMoveSpeed;
        SpeedSet = null;
    }

    public void SetSpeed(float speed, float time)
    {
        SpeedSet = StartCoroutine(SpeedDown(speed, time));
    }

    public void Ondamage(float Damage)
    {
        Hp -= Damage;
        if (Hp <= 0.0f)
        {
            Hp = 0.0f;
            myAnim.SetTrigger("Death");  // 쓰러지는 애니메이션 출력
            ChangeState(STATE.DEATH);
        }
        else
        {
            myAnim.SetTrigger("Hit");  // 맞았을 때 애니메이션 출력
            Down = false;
            OnHide = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if ((InterMask & 1 << other.gameObject.layer) != 0)
        {
            myStock = other.GetComponent<SStock_Shelves>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(myStock != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                myStock.GetComponent<SStock_Shelves>().Displaying(MyMap);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myStock = null;
    }

}
