using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SConsumer : MonoBehaviour
{
    Animator _anim;
    Animator myAnim
    {
        get 
        { 
            if(_anim == null)
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

    NavMeshAgent myNav;
    public SNPCsummoner mySummoner;
    List<Transform> BuyingList;
    int NextState = 0;
    int EntranceNum = 0;


    public enum STATE
    {
        NONE,CREATE, ENTRANCE, IDLE, LOOKAROUND, CHOICE, BUYING, OUT
            
    }
    public STATE myState;
    public int myGold;
    float Dist = 0.0f;
    public Transform BuyingItem = null;
    float ToItemDist = 0.0f;
    float delayTime = 0.0f;
    int price = 0;
    bool Trade = false;
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


    public void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.CREATE:
                myGold = Random.Range(2000, 3000);
                myNav = this.GetComponent<NavMeshAgent>();
                BuyingList = new List<Transform>();
                //Animation �۵��� ���Ŀ� ���� �����ӵ� delegate�� ����
                myAnimEvent.Move += () =>
                {
                    myAnim.SetBool("StopMove", false);
                    switch(myState)
                    {
                        case STATE.LOOKAROUND:
                            if (BuyingList.Count > 0)
                            {
                                ChangeState(STATE.CHOICE);
                            }
                            else
                            {
                                ChangeState(STATE.OUT);
                            }
                            break;
                        case STATE.IDLE:
                            if(Trade)
                            {
                                BuyingItem.GetComponent<SStock_Shelves>().OutStockItem();
                                ChangeState(STATE.BUYING);
                            }
                            else
                            {
                                ChangeState(STATE.OUT);
                            }
                            break;
                    }

                };
                break;
            case STATE.ENTRANCE:
                myAnim.SetBool("IsWalk", true);
                EntranceNum = Random.Range(1,4);
                myNav.SetDestination(mySummoner.MovePosition[EntranceNum].transform.position);
                break;
            case STATE.IDLE:
                myAnim.SetBool("IsWalk", false);
                break;
            case STATE.LOOKAROUND:
                myAnim.SetTrigger("LookAround");
                myAnim.SetBool("StopMove", true);
                foreach(Transform t in mySummoner.myStock)
                {
                    if (!t.GetComponent<SStock_Shelves>().GetDisplaying()) continue;

                    if (myGold > t.GetComponent<SStock_Shelves>().price && !t.GetComponent<SStock_Shelves>().GetInUse())
                    {
                        BuyingList.Add(t);
                    }
                }
                break;
            case STATE.CHOICE:
                while (BuyingItem == null)
                {
                    if (BuyingList.Count == 0) break;

                    BuyingItem = BuyingList[Random.Range(0, BuyingList.Count)];

                    if (BuyingItem.GetComponent<SStock_Shelves>().GetInUse())
                    {
                        BuyingList.Remove(BuyingItem);
                        BuyingItem = null;
                    }
                }
                if (BuyingItem != null)
                {
                    BuyingItem.GetComponent<SStock_Shelves>().SetInUse(true);

                    myNav.SetDestination(BuyingItem.transform.position);
                    myAnim.SetBool("IsWalk", true);
                }
                // ������ �� �ִ� ����� �������� �ϳ��� �̵�
                break;
            case STATE.BUYING:
                MoveToPosition();
                break;
            case STATE.OUT:
                myNav.isStopped = false;
                MoveToPosition();
                break;
        }
    }

    public void StateProcess()
    {

        switch (myState)
        {
            case STATE.CREATE:
                if (mySummoner.MovePosition.Length > 0)
                    ChangeState(STATE.ENTRANCE);
                break;
            case STATE.ENTRANCE:
                Dist = (mySummoner.MovePosition[EntranceNum].position- this.transform.position).magnitude;
                if(Dist <= myNav.stoppingDistance + 0.1f)
                {
                    ChangeState(STATE.IDLE);
                    NextState = 4;
                }
                break;
            case STATE.IDLE:
                if (delayTime > 0.0f)
                {
                    delayTime -= Time.deltaTime;
                }
                else
                {
                    delayTime = 2.0f;
                    switch (NextState)
                    {
                        case 4:
                            ChangeState(STATE.LOOKAROUND);
                            NextState= 0;
                            break;
                        case 5:
                            ChangeState(STATE.CHOICE);
                            NextState = 0;
                            break;
                        case 6:
                            if (Trade)
                            {
                                myAnim.SetTrigger("Yes");  // �ִϸ��̼� ������ buying���� �˾Ƽ� �Ѿ
                            }
                            else
                            {
                                myAnim.SetTrigger("No");
                            }
                            NextState = 0;
                            break;
                        case 7:
                            ChangeState(STATE.OUT);
                            NextState = 0;
                            break;
                    }
                }
                break;
            case STATE.LOOKAROUND:
                break;
            case STATE.CHOICE:
                if(BuyingItem == null)
                {
                    ChangeState(STATE.OUT);  // �� �������� �ٸ� ����� ���� �ڸ��� �� ���
                    break;
                }
                ToItemDist = (BuyingItem.position - this.transform.position).magnitude;
                if (ToItemDist <= myNav.stoppingDistance + 0.1f)
                {
                    myAnim.SetBool("IsWalk", false);
                    price = BuyingItem.GetComponent<SStock_Shelves>().price;
                    if (price < 1000) //1000�� �ü��� ���Ƿ� ������ ��
                    {
                        // �츸�� �����̶�� ���� �����̴� �ִϸ��̼� ����
                        Trade = true;
                        ChangeState(STATE.IDLE);
                        NextState = 6;
                        
                    }
                    else
                    {
                        // ��δٸ� �� ���� �ִϸ��̼� ����
                        Trade = false;
                        ChangeState(STATE.IDLE);
                        NextState = 6;
                    }
                }
                    break;
            case STATE.BUYING:
                float RegDist = (mySummoner.MovePosition[4].position - this.transform.position).magnitude;
                if(RegDist <= myNav.stoppingDistance )
                {
                    myAnim.SetBool("IsWalk", false);
                    myNav.ResetPath();
                    myNav.isStopped = true;
                    
                    float Dot = Vector3.Dot(this.transform.forward, mySummoner.MovePosition[4].forward);

                    if (Dot <= 1.0f)
                    { 
                        transform.rotation = Quaternion.Slerp(transform.rotation, mySummoner.MovePosition[4].rotation, Time.deltaTime * 5.0f);
                    }
                    else if(Trade)
                    {
                        Trade = false;
                        Debug.Log(this.gameObject.name + "����" + price + "gold�� �޾ҽ��ϴ�.");
                        ChangeState(STATE.IDLE);
                        NextState = 7;
                    }

                }
                
                break;
            case STATE.OUT:
                float outDist = (mySummoner.MovePosition[0].transform.position - this.transform.position).magnitude;
                if (outDist <= myNav.stoppingDistance + 0.1f)
                {
                    myAnim.SetBool("IsWalk", false);
                    Destroy(this.gameObject,3.0f);
                }
                break;
        }
    }

    public void MoveToPosition()
    {
        myAnim.SetBool("IsWalk", true);
        switch(myState)
        {
            case STATE.BUYING:
                myNav.SetDestination(mySummoner.MovePosition[4].position);
                break;
            case STATE.OUT:
                myNav.SetDestination(mySummoner.MovePosition[0].position);
                break;
        }
    }
}
