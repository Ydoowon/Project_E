using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STrap_Fire : MonoBehaviour
{
    float Damage = 5.0f;
    ParticleSystem[] Fire;
    // Start is called before the first frame update
    void Start()
    {
        Fire = GetComponentsInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Fire[0].Play();
            other.GetComponent<SPlayer>().OnDotDamage(Damage, Fire[1].gameObject);
        }
    }

}
