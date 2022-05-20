using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disssolve : MonoBehaviour
{
    public Animator myani;

    // Start is called before the first frame update
    void Start()
    {
        myani = this.GetComponent<Animator>();
    }

    public void Open()
    {
        myani.SetTrigger("Disssolve");
        Destroy(gameObject);
    }
}
