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
        if(_additem.ItemData.Countable)  // �� �� �ִ� �������� �߰��� ��
        {
            for(int i = 0; i < myItemSlot.Length; i++)
            {
                if (myItemSlot[i].myItem == null) continue; // ���Կ� �������� ������ ��ŵ
                //�߰��Ϸ��� �����۰� �ߺ��Ǵ� �������� ���� ��� && ������ ������ ������ ���� ��
                if (myItemSlot[i].myItem.ItemData.Name == _additem.ItemData.Name && (myItemSlot[i].ItemCount < 11 - count))
                {
                    myItemSlot[i].UpdateItem(_additem, count, true);
                    // ������ ī��Ʈ �÷��ְ�, �ؽ�Ʈ�� ����
                    return;
                }
            }
            // �� ĭ�� ������ �����ؾ� �ϴ� ���
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
        else // �� �� ���� �������� �߰��� ��
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

    // ������ ���Կ��� �Ű� ������ �޾ƿ� �������� �ִ��� Ȯ���Ѵ�.
    // �ִٸ� ������ ���� ��ȣ�� return ���ְ�, ���ٸ� -1�� �������ش�.
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
