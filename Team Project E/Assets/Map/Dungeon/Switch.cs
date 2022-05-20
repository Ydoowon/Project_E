using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public GameObject Hidden;
    Animator HidAni;

    void Start()
    {
        HidAni = Hidden.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // ���� ������ �ʾ������� �۵�
        {
                other.GetComponent<SPlayer>().Switch = this;
        }
    }

    public void Open()
    {
        HidAni.SetTrigger("Dissolve");
        StartCoroutine(DelayTime(3));
    }

    IEnumerator DelayTime(float Time)
    {
        yield return new WaitForSeconds(Time);
        Destroy(Hidden);
    }
}
