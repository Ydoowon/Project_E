using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class STransPosition : MonoBehaviour
{
    public Transform WarpPos;
    [SerializeField]
    [TextArea]
    string Message;

    SPlayer Player;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SGameManager.instance.PressE.SetActive(true);
            SGameManager.instance.Message.text = Message;
            Player = other.gameObject.GetComponent<SPlayer>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player = null;
            SGameManager.instance.PressE.SetActive(false);
        }
    }

    private void Update()
    {
        if(Player!=null && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeInFadeOut(Player.gameObject));
        }
            
    }

    IEnumerator FadeInFadeOut(GameObject Player)
    {
        SGameManager.instance.FadeInOut();
        yield return new WaitForSeconds(1.0f);
        Player.GetComponent<NavMeshAgent>().Warp(WarpPos.position);
        Player.GetComponent<SPlayer>().myPlayer.rotation = WarpPos.rotation;
    }
}
