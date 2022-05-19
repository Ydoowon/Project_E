using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STrap : MonoBehaviour
{
    [SerializeField]
    float Damage = 5.0f;
    Animator _anim;
    Animator myAnim
    {
        get
        {
            if(_anim == null)
            {
                _anim = GetComponent<Animator>();
            }
            return _anim;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            myAnim.SetTrigger("Step"); // ¿€µø
            other.GetComponent<SPlayer>().Ondamage(Damage);
        }
    }
}
