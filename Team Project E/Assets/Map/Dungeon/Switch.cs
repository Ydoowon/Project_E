using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Switch : MonoBehaviour
{

    public GameObject Hidden;
    Animator HidAni;

    [SerializeField]
    [TextArea]
    string Message;

    SPlayer Player;

    void Start()
    {
        HidAni = Hidden.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) // 문이 열리지 않았을때만 작동
        {
                other.GetComponent<SPlayer>().Switch = this;
            SGameManager.instance.PressE.SetActive(true);
            SGameManager.instance.Message.text = Message;
            Player = other.gameObject.GetComponent<SPlayer>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player = null;
            SGameManager.instance.PressE.SetActive(false);
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
