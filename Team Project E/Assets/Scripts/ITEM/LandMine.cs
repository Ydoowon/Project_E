using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    public float Dmamge; 
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) //�浹��
    {
        if(other.gameObject.tag == "Untagged") //�±��� �̸����� �Ǵ�
        {
           
            Destroy(this.gameObject);
        }
    }
}
