using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SDungeonTranspos : MonoBehaviour
{
    Transform DestPos = null;

    [SerializeField]
    [TextArea]
    string Message;
    SPlayer Player;
    bool IsActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player = other.gameObject.GetComponent<SPlayer>();
            SGameManager.instance.PressE.SetActive(true);
            SGameManager.instance.Message.text = Message;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            IsActive = false;
            Player = null;
            SGameManager.instance.PressE.SetActive(false);
            SGameManager.instance.DungeonSelectUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null && Input.GetKeyDown(KeyCode.E))
        {
            IsActive = !IsActive;
            SGameManager.instance.PressE.SetActive(!IsActive);
            SGameManager.instance.DungeonSelectUI.SetActive(IsActive);
        }
    }

    public void MoveToDungeon()
    {
        if (Player != null && DestPos != null)
        {
            SGameManager.instance.PressE.SetActive(false);
            SGameManager.instance.DungeonSelectUI.SetActive(false);
            StartCoroutine(FadeInFadeOut(Player.gameObject));
        }

    }
    IEnumerator FadeInFadeOut(GameObject Player)
    {
        SGameManager.instance.FadeInOut();
        yield return new WaitForSeconds(1.0f);
        Player.GetComponent<NavMeshAgent>().Warp(DestPos.position);
        Player.GetComponent<SPlayer>().myPlayer.rotation = DestPos.rotation;
    }
    public void SetDestPos(Transform Dest)
    {
        DestPos = Dest;
    }
}
