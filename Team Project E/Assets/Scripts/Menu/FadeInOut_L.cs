using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut_L : MonoBehaviour
{
    public Image FadeImage;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }


    public void StartFadeOut()
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
    }

    IEnumerator FadeIn()
    {
        float FadeTime = 1.0f;
        while (FadeTime > 0.0f)
        {
            FadeTime -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            FadeImage.color = new Color(0, 0, 0, FadeTime);
        }
    }
}
