using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomButton_L : MonoBehaviour ,IPointerEnterHandler ,IPointerExitHandler
{

    public Image myImage = null;
    public Button myButton = null;
    public int myImageNum = 0;
    public int ButtonNum = 0;
    bool Map_Up = false;
    bool Map_Down = false;
    bool Map_Left = false;
    bool Map_Right = false;
    public SPlayer myPlayer;

    public List<Sprite> ImgList = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MapChange();
    }

    public void MapChange()
    {
        if (myButton.interactable && Input.GetKeyDown(KeyCode.DownArrow))
        {
            Map_Down = !Map_Down;
            if (Map_Down == true)
            {
                DrowMap(myImageNum + 1);
                myPlayer.SetMapData(ButtonNum, myImageNum);
            }
            else
            {
                DrowMap(myImageNum - 1);
                myPlayer.SetMapData(ButtonNum, myImageNum);
            }
        }

        if (myButton.interactable && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Map_Up = !Map_Up;
            if (Map_Up == true)
            {
                DrowMap(myImageNum + 2);
                myPlayer.SetMapData(ButtonNum, myImageNum);
            }

            else
            {
                DrowMap(myImageNum - 2);
                myPlayer.SetMapData(ButtonNum, myImageNum);
            }
        }
        if (myButton.interactable && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Map_Left = !Map_Left;
            if (Map_Left == true)
            {
                DrowMap(myImageNum + 4);
                myPlayer.SetMapData(ButtonNum, myImageNum);
            }

            else
            {
                DrowMap(myImageNum - 4);
                myPlayer.SetMapData(ButtonNum, myImageNum);
            }
        }
        if (myButton.interactable && Input.GetKeyDown(KeyCode.RightArrow))
        {
            Map_Right = !Map_Right;
            if (Map_Right == true)
            {
                DrowMap(myImageNum + 8);
                myPlayer.SetMapData(ButtonNum, myImageNum);
            }

            else
            {
                DrowMap(myImageNum - 8);
                myPlayer.SetMapData(ButtonNum, myImageNum);
            }
        }
    }
    public void DrowMap(int ImageNum)
    {
        if (myImageNum == ImageNum) return;
        myImageNum = ImageNum;
        myImage.sprite = ImgList[myImageNum];
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        myButton.interactable = true;
        myButton.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myButton.interactable = false;
        myButton.transform.localScale = new Vector3(1, 1, 1);
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    myImage = Resources.Load("Image/MapData/LB") as Image;
    //}

}
