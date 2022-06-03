using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager_L : MonoBehaviour
{
    public Stack<GameObject> PopUpList = new Stack<GameObject>();


    public GameObject myInven;
    public SItemSlot[] myItemSlot;
    public GameObject myMap;
    [SerializeField]
    RoomButton_L[] myButtons;
    public Canvas myCanvas;

    public M_menu Menu;

    bool ActiveInven = false;
    bool ActiveMap = false;

    public Transform myPlayer;
    [SerializeField]
    SPlayer player;
    public Image myCompass;
    public MapName myMapname;

    void Start()
    {
        myInven.SetActive(ActiveInven);
        myMap.SetActive(ActiveMap);
        myItemSlot = myInven.GetComponentsInChildren<SItemSlot>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && PopUpList.Count == 0)
        {
            InvenOnOff();
        }

        if (Input.GetKeyDown(KeyCode.M) && PopUpList.Count == 0)
        {
            MapOnOff();
        }
        if (myCompass.gameObject.activeSelf)
        {
            SetCompass();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PopUpList.Count == 0)
            {
                PopUpList.Push(Menu.gameObject);
                MenuOnOff();
            }
            else
            {
                PopToList();
                if (PopUpList.Count == 0)
                {
                    Menu.Close();
                }
            }
        }
    }
    void InvenOnOff()
    {
        ActiveInven = !ActiveInven;
        myInven.SetActive(ActiveInven);
    }

    void MapOnOff()
    {
        ActiveMap = !ActiveMap;
        myMap.SetActive(ActiveMap);
    }
    public void MenuOnOff()
    {
        Menu.gameObject.SetActive(true);
        Menu.Open();
    }


    public void SetMyButton(int Row, int Col)
    {

        if (myButtons != null)
        {
            myButtons[(Row - 1) * 4 + Col - 1].gameObject.SetActive(true);
            player.GetmyMap().myRooms[(Row - 1) * 4 + Col - 1].Checking = true;
            ActiveCompass(true);
            myCompass.transform.SetParent(myButtons[(Row - 1) * 4 + Col - 1].transform);
            myCompass.rectTransform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void ActiveCompass(bool Active)
    {
        if (myCompass.gameObject.activeSelf == Active) return;
        myCompass.gameObject.SetActive(Active);
    }

    // ui상 맵 넘버, 이미지, 보일지 안보일지 매개변수로 받음
    public void MapLoad(int num, int ImageNum, bool Checking)
    {
        myButtons[num].gameObject.SetActive(Checking);
        myButtons[num].DrowMap(ImageNum);
    }

    public void SetCompass()
    {
        if (myCompass != null)
            myCompass.rectTransform.localRotation = Quaternion.Euler(0, 0, -myPlayer.transform.localRotation.eulerAngles.y);
    }
    public void AddItem(SItem _additem, int count = 1, int price = 0)
    {
        if (!AddAvailable(_additem, count)) return;

        if (_additem.ItemData.Countable)  // 셀수 있는 아이템을 추가하는 경우
        {
            for (int i = 0; i < myItemSlot.Length; i++) // 이미 아이템을 보유하고 있는지 체크
            {
                if (myItemSlot[i].myItem == null) continue; // 아이템이 없는 슬롯은 넘어감
                // 추가해야 하는 아이템을 찾았을 경우 && count가 가득차지 않았을 때
                if (myItemSlot[i].myItem.ItemData.Name == _additem.ItemData.Name && (myItemSlot[i].ItemCount < 10))
                {
                    if(count > 10 - myItemSlot[i].ItemCount)
                    {
                        int itemcnt = myItemSlot[i].ItemCount;
                        myItemSlot[i].UpdateItem(10 - myItemSlot[i].ItemCount, true);  // 10개 맞춰서 업데이트
                        AddItem(_additem, count + itemcnt - 10); // 나머지는 다음 슬롯에서 업데이트
                    }
                    else 
                    {
                        myItemSlot[i].UpdateItem(count, true);
                    }
                    return;
                }
            }   
            // 셀 수 있는 아이템을 '새로' 추가 하는 경우
            for (int i = 0; i < myItemSlot.Length; i++)
            {
                if (myItemSlot[i].myItem == null)
                {
                    if (count > 10)
                    {
                        InstItem(_additem, i, price);
                        myItemSlot[i].UpdateItem(10, true);
                        AddItem(_additem, count - 10);
                    }
                    else
                    {
                        InstItem(_additem, i, price);
                        myItemSlot[i].UpdateItem(count, true);
                    }
                    return;
                }
            }
        }
        else // 셀 수 없는 아이템을 추가하는 경우
        {
            for (int i = 0; i < myItemSlot.Length; i++)
            {
                if (myItemSlot[i].myItem == null)
                {
                    InstItem(_additem, i, price);
                    myItemSlot[i].UpdateItem(count, false);
                    return;
                }
            }
        }
    }

    public void InstItem(SItem _additem, int index, int price = 0)
    {

        GameObject obj = Instantiate(_additem.gameObject, myItemSlot[index].gameObject.transform);
        obj.GetComponent<Image>().sprite = obj.GetComponent<SItem>().ItemData.Image;
        obj.GetComponent<RectTransform>().localScale = Vector3.one;

        obj.transform.SetAsFirstSibling();
        obj.GetComponent<SItem>().Price = price;
        obj.GetComponent<SItem>().CanvasSF = myCanvas.scaleFactor;
    }

    public int FindItem(SItem findItem)
    {
        for (int i = 0; i < myItemSlot.Length; i++)
        {
            if (myItemSlot[i].myItem == null) continue;

            if (myItemSlot[i].myItem.ItemData.Name == findItem.ItemData.Name)
            {
                return i;
            }
        }
        return -1;
    }

    public bool AddAvailable(SItem item, int additemcnt)
    {
        int Count = myItemSlot.Length * 10;
        
        if (item.ItemData.Countable)
        {
            for (int i = 0; i < myItemSlot.Length; i++)
            {
                if (myItemSlot[i].myItem == null) continue;

                if (myItemSlot[i].myItem.ItemData.Name == item.ItemData.Name)
                {
                    Count -= myItemSlot[i].ItemCount;
                    if (Count < additemcnt) return false;
                }
                else
                {
                    Count -= 10;
                    if (Count < additemcnt) return false;
                }
            }
            return Count >= additemcnt ? true : false;
        }
        else
        {
            for (int i = 0; i < myItemSlot.Length; i++)
            {
                if (myItemSlot[i].myItem != null)
                    Count -= 10;
            }
            return Count > 0 ? true : false;
        }
    }

    public void PushTolist(GameObject popup)
    {
        PopUpList.Push(popup);
    }
    public void PopToList()
    {
        PopUpList.Pop().SetActive(false);
    }
}
