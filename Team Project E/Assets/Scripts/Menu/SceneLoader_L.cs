using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader_L : MonoBehaviour
{
    static SceneLoader_L _inst = null;
    public static SceneLoader_L Inst
    {
        get
        {
            if(_inst == null)
            {
                _inst = FindObjectOfType<SceneLoader_L>();
                if(_inst == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SceneLoader";
                    DontDestroyOnLoad(obj);
                    _inst = obj.AddComponent<SceneLoader_L>();

                }
            }
            return _inst;
        }
    }

    public void LoadScene(int i)
    {
        StartCoroutine(SceneLoading(i));
        
    }

    IEnumerator SceneLoading(int i)
    {
        yield return SceneManager.LoadSceneAsync(1);
        yield return StartCoroutine(Loading(i));
    }

    IEnumerator Loading(int i)
    {
        Slider loadingbar = GameObject.Find("LoadingProgress")?.GetComponent<Slider>();
        AsyncOperation ao =  SceneManager.LoadSceneAsync(i);
        //씬 로딩이 끝나기 전까지 씬을 활성화 하지 않는다
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            float v = Mathf.Clamp01(ao.progress / 0.9f);
            if (loadingbar != null) loadingbar.value = v;
            if (Mathf.Approximately(v, 1.0f))
            {
                ao.allowSceneActivation = true;
            }
            yield return null;
        }
    }

    public void LoadScene(string sceneName)
    {

    }

}
