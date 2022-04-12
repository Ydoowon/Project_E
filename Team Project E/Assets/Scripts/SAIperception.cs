using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAIperception : MonoBehaviour
{
    public LayerMask myEnemyMask;
    public SMonster myMonster;
    public List<GameObject> myEnemyList = new List<GameObject>();
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
            myEnemyList.Add(other.gameObject); // ���߿� �÷��̾�� �ɹڼ� �ý��� �۵���Ű�� ���� ���� ������Ʈ ���ص���
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        myEnemyList.Remove(other.gameObject); // �������� ���������� ����Ʈ���� �����
    }

}
