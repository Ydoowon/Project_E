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
    public void MapSetting(Map UserMap) // 매개변수로 받은 유저 맵으로 UI맵을 새팅한다
    {
        // 유저 맵 크기만큼 반복(한칸 한칸 그려야 하기 때문에)
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
            //기존에 플레이어가 가지고 있던 지도가 있을 경우 추가하지 않음
            for(int i = 0; i<Player.myMapList.Count; i++)
            {
                if(Player.myMapList[i].Mapnum == _themap.Mapnum)
                {
                    Player.UsingMapNum = i;
                    MapSetting(Player.myMapList[i]);
                    return;
                }
            }
            // 없으면 새로 추가함
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

        // 단편적인 정보 저장
        SaveData PlayerData = new SaveData();
        PlayerData.PlayerPos = Player.transform.position;  // 위치
        PlayerData.PlayerRot = Player.myPlayer.rotation.eulerAngles;  // 회전값
        PlayerData._level = Player.MyStatus.PlayerLevel;  // 레벨
        PlayerData._exp = Player.MyStatus.Exp;  // 경험치
        PlayerData._hp = Player.MyStatus.HP;  // hp상태
        PlayerData._hidepoint = Player.MyStatus.Hidepoint;  // hidepoint 상태
        PlayerData._gold = Player.MyStatus.Gold;   // 골드
        PlayerData._usingMapNum = Player.UsingMapNum;  // 어떤 맵 사용하고 있었는지
        
        //플레이어가 가지고 있던 맵 데이터 저장
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

        //상점에 올려둔 아이템 저장
        ShowMessage("저장됐습니다");
        string json = JsonUtility.ToJson(PlayerData);

        string filename = "savedata";
        string path = Application.dataPath + "/" + filename + ".Json";
        File.WriteAllText(path, json);
    }
}
