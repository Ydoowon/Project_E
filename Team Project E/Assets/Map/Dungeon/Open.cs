using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    Animator myani;
    [SerializeField]
    float Lockgauge = 100.0f;
    public bool DoorOpen = false;  

    // Start is called before the first frame update
    void Start()
    {
        myani = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            myani.SetTrigger("Open");
            StartCoroutine(CloseWait());
        }
        */
    }
    /*
    IEnumerator CloseWait()
    {
        yield return new WaitForSeconds(CloseWaiting);
        myani.SetTrigger("Close");
    }
    */

    public void DoorUnlock(float speed)
    {
        Lockgauge -= Time.deltaTime * speed;
        if (Lockgauge <= 0)
        {
            Lockgauge = 0.0f;
            myani.SetTrigger("Open");
            DoorOpen = true;
        }
    }

}
