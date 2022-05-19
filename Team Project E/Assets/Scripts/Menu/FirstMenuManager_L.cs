using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstMenuManager_L : MonoBehaviour
{

    public GameObject FirstMenu;
    public Image FadeImage;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TitleNewStart()
    {
        StartCoroutine(Fadeout());
    }
    public void TitleContinue()
    {
        GameObject odj = Instantiate(Resources.Load("UI/Canvas_Countinue")) as GameObject;
        Destroy(FirstMenu);
    }
    public void TitleOption()
    {
        GameObject odj = Instantiate(Resources.Load("UI/Canvas_OptionMenu")) as GameObject;
        Destroy(FirstMenu);
    }
    public void TitleEnd()
    {
        Application.Quit();
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
