using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrap : MonoBehaviour
{
    public Transform TargetPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
         TargetPosition.position = new Vector3(Random.Range(-60.0f, 60.0f), 60.0f, 0.0f);

        Destroy(this.gameObject);
    }
}
