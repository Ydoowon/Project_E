using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    Animator myani;
    [SerializeField]
    float Lockgauge = 100.0f;
    public bool DoorOpen = false;  

    void Start()
    {
        myani = this.GetComponent<Animator>();
    }

    void Update()
    {
    }

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
