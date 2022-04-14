using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAIperception : MonoBehaviour
{
    public LayerMask myEnemyMask;
    public SMonster myMonster;
    public List<GameObject> myEnemyList = new List<GameObject>();

    Coroutine FindRoutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if((myEnemyMask & (1 << other.gameObject.layer)) != 0 )
        {
            if (FindRoutine != null)
            {
                StopCoroutine(FindRoutine);
                if(myEnemyList.Count>0)
                    myEnemyList.Clear();
            }

            if(other.gameObject.GetComponent<SPlayer>().myState != SPlayer.STATE.DEATH)
            myEnemyList.Add(other.gameObject); // ���߿� �÷��̾�� �ɹڼ� �ý��� �۵���Ű�� ���� ���� ������Ʈ ���ص���
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        FindRoutine = StartCoroutine(RemoveTarget(other));
    }

    IEnumerator RemoveTarget(Collider other)
    {
        yield return new WaitForSeconds(3.0f);
        myEnemyList.Remove(other.gameObject);
    }

}
