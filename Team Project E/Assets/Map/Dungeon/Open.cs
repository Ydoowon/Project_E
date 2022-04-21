using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    public LayerMask DoorButton;
    public Animator myani;
    public float CloseWaiting = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myani.SetTrigger("Open");
            StartCoroutine(CloseWait());
        }
    }

    IEnumerator CloseWait()
    {
        yield return new WaitForSeconds(CloseWaiting);
        myani.SetTrigger("Close");
    }
}
