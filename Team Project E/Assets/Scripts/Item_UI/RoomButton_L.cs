using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomButton_L : MonoBehaviour ,IPointerEnterHandler ,IPointerExitHandler
{

    public Image myImage = null;
    public Button myButton = null;
    public int ImageNum = 0;
    bool Map_Up = false;
    bool Map_Down = false;
    bool Map_Left = false;
    bool Map_Right = false;


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
        if (myButton.interactable && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Map_Down = !Map_Down;
            if (Map_Down == true)
            {
                ImageNum = ImageNum + 1;
            }
            else
            {
                ImageNum = ImageNum - 1;
            }
        }
        if (myButton.interactable && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Map_Up = !Map_Up;
            if (Map_Up == true)
            {
                ImageNum = ImageNum + 2;
            }

            else
            {
                ImageNum = ImageNum - 2;
            }
        }
        if (myButton.interactable && Input.GetKeyDown(KeyCode.Alpha3))
        {
            Map_Left = !Map_Left;
            if (Map_Left == true)
            {
                ImageNum = ImageNum + 4;

            }

            else
            {
                ImageNum = ImageNum - 4;

            }

            //myImage.sprite = Resources.Load<Sprite>("Image/MapData/R") as Sprite;
        }
        if (myButton.interactable && Input.GetKeyDown(KeyCode.Alpha4))
        {
            Map_Right = !Map_Right;
            if (Map_Right == true)
            {
                ImageNum = ImageNum + 8;

            }

            else
            {
                ImageNum = ImageNum - 8;

            }

            //myImage.sprite = Resources.Load<Sprite>("Image/MapData/T") as Sprite;
        }

        switch(ImageNum)
        {
            case 0:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/Null") as Sprite;
                break;
            case 1:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/B") as Sprite;
                break;
            case 2:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/T") as Sprite;
                break;
            case 3:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/TB") as Sprite;
                break;
            case 4:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/L") as Sprite;
                break;
            case 5:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/LB") as Sprite;
                break;
            case 6:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/TL") as Sprite;
                break;
            case 7:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/TLB") as Sprite;
                break;
            case 8:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/R") as Sprite;
                break;
            case 9:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/RB") as Sprite;
                break;
            case 10:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/TR") as Sprite;
                break;
            case 11:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/TRB") as Sprite;
                break;
            case 12:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/RL") as Sprite;
                break;
            case 13:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/RLB") as Sprite;
                break;
            case 14:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/TRL") as Sprite;
                break;
            case 15:
                myImage.sprite = Resources.Load<Sprite>("Image/MapData/TRLB") as Sprite;
                break;

        }
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
