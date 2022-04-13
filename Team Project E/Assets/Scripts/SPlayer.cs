using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SPlayer : MonoBehaviour
{
    public float MoveSpeed = 5.0f;
    float OriginMoveSpeed = 5.0f;

    float hAxis;
    float vAxis;
    bool Running;
    Coroutine SpeedSet;

    public float Hp = 100.0f;  // player HP
    public float HidePoint = 100.0f; // player ������ �ִ� �ð�

    public Transform myPlayer;
    public STATE myState = STATE.NONE;
    public bool OnHide = false;

    public List<Item> Itemlist = new List<Item> ();  // �� ������ ����Ʈ


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
                myAnimEvent.StandUp += () => OnHide = false;  // ���� ���� �����ǵ��� �ϴ� delegate ����
                ChangeState(STATE.PLAY); // ������ Play STATE�� ����
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

                if (!OnHide)  // ���� ���°� �ƴҶ��� �̵� ����
                {
                    hAxis = Input.GetAxis("Horizontal");
                    vAxis = Input.GetAxis("Vertical");
                    Running = Input.GetButton("Run");
                    Vector3 pos = new Vector3(hAxis, 0, vAxis).normalized;
                    Moving(pos);
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
        myAnim.SetBool("IsRun", Running);
        myPlayer.LookAt(myPlayer.transform.position + pos);

        if (SpeedSet == null)
            MoveSpeed = myAnim.GetBool("IsRun") ? OriginMoveSpeed : OriginMoveSpeed / 2;  //Run ���¸� 5.0f, �ƴϸ� ����

        this.transform.Translate(pos * MoveSpeed * Time.deltaTime); // �̵�  
    }

    public void Hiding()
    {
        if (OnHide == false)  // ���� ���� ��� ���´�
        {
            myAnim.SetTrigger("Hiding");  // Hiding �ִϸ��̼� ����
            OnHide = true;
        }
        else
        {
            myAnim.SetTrigger("StandUp");
        }

    }

    void HideSystem()
    {
        if (OnHide)
        {
            HidePoint -= Time.deltaTime * 5.0f;
        }

        if (HidePoint <= 0 && OnHide)  // Hidepoint�� 0�̰�, ���� ������ ��
        {
            myAnim.SetTrigger("StandUp"); // ������ ������ �Ͼ�� ����
        }

        if (!OnHide && HidePoint < 100.0f)
        {
            HidePoint += Time.deltaTime; // ���� ���°� �ƴ϶�� hidepoint �ִ�ġ���� ȸ��

        }

        HidePoint = Mathf.Clamp(HidePoint, 0.0f, 100.0f);
    }

    IEnumerator SpeedDown(float speed, float time)
    {
        if (MoveSpeed > speed)  // ���� ������ ������� ���� �� �ֵ��� �ϱ� ���� ����
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
            myAnim.SetTrigger("Death");  // �������� �ִϸ��̼� ���
            ChangeState(STATE.DEATH);
        }
        else
        {
            myAnim.SetTrigger("Hit");  // �¾��� �� �ִϸ��̼� ���
        }
    }



}
