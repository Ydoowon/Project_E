using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMap : MonoBehaviour
{
    [SerializeField]
    int price;  // ���� ����

    // �÷��̾� ���� �׽�Ʈ�� ���� �ӽ� ��ũ��Ʈ�Դϴ�.
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPrice()
    {
        return price;
    }
    public void SetPrice(int _price)
    {
        price = _price;
    }
}
