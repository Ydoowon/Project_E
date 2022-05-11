using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SGameManager : MonoBehaviour
{
    static public SGameManager instance;

    public Animator FadeAnim;
    public GameObject ItemToolTip;
    public SItem[] Itemlist = null;
    public GameObject PressE;
    public GameObject Interaction;
    public TMPro.TMP_Text Message;
    public GameObject DungeonSelectUI;
    public UIManager_L MapUI;

    [SerializeField]
    TMPro.TMP_Text AlarmMessage;

    [SerializeField]
    SPlayer Player;

    //List<SaveData> PlayerData; 

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    public void MapSetting(Map UserMap) // �Ű������� ���� ���� ������ UI���� �����Ѵ�
    {
        // ���� �� ũ�⸸ŭ �ݺ�(��ĭ ��ĭ �׷��� �ϱ� ������)
        for(int i =0; i<UserMap.GetRoomCol()*UserMap.GetRoomRow(); i++)
        {
            int ImageNum = 0;
            ImageNum += UserMap.myRooms[i].EntDown ? 1 : 0;
            ImageNum += UserMap.myRooms[i].EntUp ? 2 : 0;
            ImageNum += UserMap.myRooms[i].EntLeft ? 4 : 0;
            ImageNum += UserMap.myRooms[i].EntRight ? 8 : 0;
            MapUI.MapLoad(i, ImageNum, UserMap.myRooms[i].Checking);
        }
    }


    public void CreateNewMap(int index)
    {
        if (this.GetComponent<SMapData>().myMaps.Count - 1 < index) return;

        Map _themap = this.GetComponent<SMapData>().myMaps[index];
        if(Player.myMapList.Count == 0)
        {
            Player.myMapList.Add(new Map(_themap.Mapnum, _themap.GetRoomRow(), _themap.GetRoomCol()));
            Player.UsingMapNum = 0;
            MapSetting(Player.myMapList[Player.myMapList.Count - 1]);
            return;
        }
        else
        {
            //������ �÷��̾ ������ �ִ� ������ ���� ��� �߰����� ����
            for(int i = 0; i<Player.myMapList.Count; i++)
            {
                if(Player.myMapList[i].Mapnum == _themap.Mapnum)
                {
                    Player.UsingMapNum = i;
                    MapSetting(Player.myMapList[i]);
                    return;
                }
            }
            // ������ ���� �߰���
            Player.myMapList.Add(new Map(_themap.Mapnum, _themap.GetRoomRow(), _themap.GetRoomCol()));
            Player.UsingMapNum = Player.myMapList.Count - 1;
            MapSetting(Player.myMapList[Player.myMapList.Count - 1]);
        }
    }


    public void ShowMessage(string Message)
    {
        AlarmMessage.text = Message;
        AlarmMessage.GetComponent<Animator>().SetTrigger("ShowMessage");
    }


    public void Save(int SaveSlotNum)
    {

        // �������� ���� ����
        SaveData PlayerData = new SaveData();
        PlayerData.PlayerPos = Player.transform.position;  // ��ġ
        PlayerData.PlayerRot = Player.myPlayer.rotation.eulerAngles;  // ȸ����
        PlayerData._level = Player.MyStatus.PlayerLevel;  // ����
        PlayerData._exp = Player.MyStatus.Exp;  // ����ġ
        PlayerData._hp = Player.MyStatus.HP;  // hp����
        PlayerData._hidepoint = Player.MyStatus.Hidepoint;  // hidepoint ����
        PlayerData._gold = Player.MyStatus.Gold;   // ���
        PlayerData._usingMapNum = Player.UsingMapNum;  // � �� ����ϰ� �־�����
        
        //�÷��̾ ������ �ִ� �� ������ ����
        PlayerData.PlayerMapList = Player.myMapList.ToArray();

        int cnt = MapUI.myItemSlot.Length;
        List<InventoryData> InvenData = new List<InventoryData>();
        for (int i = 0; i < cnt; i++)
        {
            if (MapUI.myItemSlot[i].myItem == null) continue;
            else
            {
                SItemSlot Slot = MapUI.myItemSlot[i];
                int index = Slot.myItem.ItemData.ItemIndex;
                int Count = Slot.ItemCount;

                InvenData.Add(new InventoryData(i, index, Count));
            }
        }
        PlayerData.Inventory = InvenData.ToArray();

        //������ �÷��� ������ ����
        ShowMessage("����ƽ��ϴ�");
        string json = JsonUtility.ToJson(PlayerData);

        string filename = "savedata";
        string path = Application.dataPath + "/" + filename + ".Json";
        File.WriteAllText(path, json);
    }
}
