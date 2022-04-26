using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class SGameManager : MonoBehaviour
{
    static public SGameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(1);
            SPlayer.instance.GetComponent<NavMeshAgent>().Warp(new Vector3(97.0f, 21.0f, 8.0f));
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene(0);
            SPlayer.instance.GetComponent<NavMeshAgent>().Warp(new Vector3(-340.0f, 3.0f, 180.0f));
        }
    }
}
