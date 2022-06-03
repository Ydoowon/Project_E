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
    Coroutine TransRoutine;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SGameManager.instance.PressE.SetActive(true);
            SGameManager.instance.Message.text = Message;
            Player = other.gameObject.GetComponent<SPlayer>();
            Player.TransPos += WarpPlayer;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player.TransPos -= WarpPlayer;
            Player = null;
            SGameManager.instance.PressE.SetActive(false);
        }
    }

    IEnumerator FadeInFadeOut(GameObject Player)
    {
        SGameManager.instance.FadeInOut();
        yield return new WaitForSeconds(1.0f);
        Player.GetComponent<NavMeshAgent>().Warp(WarpPos.position);
        Player.GetComponent<SPlayer>().myPlayer.rotation = WarpPos.rotation;
        Player.GetComponent<SPlayer>().SetMyLocation(PlayerStatus.LOCATION.TOWN);
        TransRoutine = null;
    }

    public void WarpPlayer(SPlayer player)
    {
        if(TransRoutine == null)
        TransRoutine = StartCoroutine(FadeInFadeOut(player.gameObject));
    }
}
