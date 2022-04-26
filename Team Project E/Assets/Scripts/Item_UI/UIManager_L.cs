using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager_L : MonoBehaviour
{
    public GameObject myInven;
    public SItemSlot[] myItemSlot;
    public GameObject myMap;
    RoomButton_L[] myButtons;


    bool ActiveInven = false;
    bool ActiveMap = false;

    void Start()
{
        myInven.SetActive(ActiveInven);
        myMap.SetActive(ActiveMap);
        myItemSlot = myInven.GetComponentsInChildren<SItemSlot>();
        myButtons = myMap.GetComponentsInChildren<RoomButton_L>();
        foreach (RoomButton_L button in myButtons)
        {
            button.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        InvenOnOff(); // Ieven On,Off
        MapOnOff();
        //Pause();      // Game Pause
    }
    void InvenOnOff()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ActiveInven = !ActiveInven;
            myInven.SetActive(ActiveInven);
        }
    }

    void MapOnOff()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ActiveMap = !ActiveMap;
            myMap.SetActive(ActiveMap);
        }
    }
    

    public void SetMyButton(int Row, int Col)
    {

        if (myButtons != null)
        {
            myButtons[(Row - 1) * 4 + Col - 1].gameObject.SetActive(true);
            SPlayer.instance.GetmyMap().myRooms[(Row - 1) * 4 + Col - 1].Checking = true;
            //this.GetComponent<PlayerstatManagement_L>().myPlayer.GetComponent<SPlayer>().GetmyMap().myRooms[(Row-1)*4+Col -1].Checking = true;
        }
    }
    public void AddItem(SItem _additem, int count = 1, int price = 0)
    {
        if(_additem.ItemData.Countable)  // 셀 수 있는 아이템을 추가할 때
        {
            for(int i = 0; i < myItemSlot.Length; i++)
            {
                if (myItemSlot[i].myItem == null) continue; // 슬롯에 아이템이 없으면 스킵
                //추가하려는 아이템과 중복되는 아이템이 있을 경우 && 아이템 슬롯이 꽉차지 않을 때
                if (myItemSlot[i].myItem.ItemData.Name == _additem.ItemData.Name && (myItemSlot[i].ItemCount < 11 - count))
                {
                    myItemSlot[i].UpdateItem(_additem, count, true);
                    // 아이템 카운트 늘려주고, 텍스트도 갱신
                    return;
                }
            }
            // 새 칸에 아이템 생성해야 하는 경우
            for(int i = 0; i < myItemSlot.Length; i++)
            {
                if(myItemSlot[i].myItem == null)
                {
                    InstItem(_additem, i, count, price);
                    myItemSlot[i].UpdateItem(_additem, count, true);
                    return;
                }
            }
        }
        else // 셀 수 없는 아이템을 추가할 때
        {
            for (int i = 0; i < myItemSlot.Length; i++)
            {
                if (myItemSlot[i].myItem == null)
                {
                    InstItem(_additem, i, count, price);
                    myItemSlot[i].UpdateItem(_additem, count, false);
                    return;
                }
            }
        }
    }

    public void InstItem(SItem _additem, int index, int count, int price = 0)
    {

        GameObject obj = Instantiate(_additem.gameObject, myItemSlot[index].gameObject.transform);
        obj.GetComponent<Image>().sprite = obj.GetComponent<SItem>().ItemData.Image;
        obj.GetComponent<RectTransform>().localScale = Vector3.one;
        
        obj.transform.SetAsFirstSibling();
        obj.GetComponent<SItem>().Price = price;

    }

    // 아이템 슬롯에서 매개 변수로 받아온 아이템이 있는지 확인한다.
    // 있다면 아이템 슬롯 번호를 return 해주고, 없다면 -1을 리턴해준다.
    public int FindItem(SItem findItem)
    {
        for(int i = 0; i < myItemSlot.Length; i++)
        {
            if (myItemSlot[i].myItem == null) continue;

            if(myItemSlot[i].myItem.name == findItem.name)
            {
                return i;
            }
        }
        return -1;
    }

    
}
