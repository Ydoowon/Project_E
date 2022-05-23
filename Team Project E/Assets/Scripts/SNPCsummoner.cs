using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNPCsummoner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] ConsumerList;
    public Transform[] MovePosition;
    public Transform[] myStock;
    public int SummonCnt; // 소환할 NPC 수
    Coroutine Summon;

    public SPlayer myPlayer;
    public enum STATE
    {
        CLOSED,OPEN
    }
    STATE myState = STATE.CLOSED;

    public void GetStateClose()
    {
        ChangeState(STATE.CLOSED);
    }
    public void GetStateOpen()
    {
        ChangeState(STATE.OPEN);
    }
    void Start()
    {
        ChangeState(STATE.CLOSED);
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
            case STATE.CLOSED:
                break;
            case STATE.OPEN:
                if (Summon == null)
                {
                    Summon = StartCoroutine(SummonConsumer());
                }
                else
                {
                    ChangeState(STATE.CLOSED);
                }

                break;
        }
    }
    public void StateProcess()
    {

        switch (myState)
        {
            case STATE.CLOSED:
                if(myPlayer != null)
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    ChangeState(STATE.OPEN);
                }
                break;
            case STATE.OPEN:
                break;
        }

    }

    IEnumerator SummonConsumer()
    {
        if (ConsumerList.Length == 0) yield break;

        while(SummonCnt > 0)
        {
            GameObject obj = Instantiate(ConsumerList[Random.Range(0, ConsumerList.Length)],this.transform);
            obj.transform.position = MovePosition[0].position;
            obj.GetComponent<SConsumer>().mySummoner = this;
            SummonCnt--;

            yield return new WaitForSeconds(Random.Range(8.0f, 10.0f));
        }
        Summon = null;
        ChangeState(STATE.CLOSED);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            myPlayer = other.gameObject.GetComponent<SPlayer>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            myPlayer = null;
        }
    }

}
