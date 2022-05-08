using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassObstacle : MonoBehaviour
{
    SkinnedMeshRenderer[] _mySkin;
    SkinnedMeshRenderer[] MySkin
    {
        get
        {
            if (_mySkin == null)
                _mySkin = this.GetComponentsInChildren<SkinnedMeshRenderer>();
            return _mySkin;
        }
        set
        {
            _mySkin = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Clock());
    }

    IEnumerator Clock()
    {
        yield return new WaitForSeconds(5f);
        for (int i = 0; i <= 60; i++)
        {
            for (int j = 0; j < MySkin.Length; j++)
            {
                MySkin[j].material.SetFloat("_DissolveAmount", (float)i / 60.0f);
            }
        }
        Destroy(gameObject);
        yield return null;
    }
}
