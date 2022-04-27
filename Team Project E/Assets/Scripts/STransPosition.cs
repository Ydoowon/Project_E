using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class STransPosition : MonoBehaviour
{
    [SerializeField]
    Vector3 WarpPosition = Vector3.zero;
    public string Destination;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(FadeInFadeOut(other.gameObject));
        }
    }

    IEnumerator FadeInFadeOut(GameObject Player)
    {
        SGameManager.instance.FadeAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.0f);
        Player.GetComponent<NavMeshAgent>().Warp(WarpPosition);
        SGameManager.instance.FadeAnim.SetTrigger("FadeIn");
    }


}
