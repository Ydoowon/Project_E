using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTipManager_L : MonoBehaviour
{
    public List<Image> myTips = new List<Image>();
    public TMPro.TMP_Text myPageNum;
    int curPage = 0;
    // Start is called before the first frame update
    void Start()
    {
        myPageNum.text = (curPage + 1) + "/ 6";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //nextPage Fuction
    public void ButtonPre()
    {
        if (curPage == 0) return;
        curPage--;
        myTips[curPage].gameObject.SetActive(true);
        myTips[curPage+1].gameObject.SetActive(false);
        myPageNum.text = (curPage+1) + "/ 6";
    }

    //PreviousPage Fuction
    public void ButtonNext()
    {
        if (curPage == 5) return;
        curPage++;
        myTips[curPage].gameObject.SetActive(true);
        myTips[curPage-1].gameObject.SetActive(false);
        myPageNum.text = (curPage+1) + "/ 6";

    }
}
