using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NewGame_L : MonoBehaviour , IPointerClickHandler
{

    public Image FadeImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(Fadeout());
    }

    IEnumerator Fadeout()
    {

        float FadeTime = 0.0f;
        while (FadeTime < 1.0f)
        {
            FadeTime += 0.01f;
            yield return new WaitForSeconds(0.01f);
            FadeImage.color = new Color(0, 0, 0, FadeTime);
        }

        SceneLoader_L.Inst.LoadScene(2);
    }


}
