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
    public enum STATE
    {
        CLOSED,OPEN
    }
    STATE myState = STATE.CLOSED;

    void Start()
    {
        ChangeState(STATE.OPEN);
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
                StartCoroutine(SummonConsumer());
                break;
        }
    }
    public void StateProcess()
    {

        switch (myState)
        {
            case STATE.CLOSED:
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
            GameObject obj = Instantiate(ConsumerList[Random.Range(0, ConsumerList.Length)], this.transform);
            obj.transform.position = MovePosition[0].position;
            obj.GetComponent<SConsumer>().mySummoner = this;
            SummonCnt--;

            yield return new WaitForSeconds(Random.Range(8.0f, 10.0f));
        }
        ChangeState(STATE.CLOSED);
    }

}
