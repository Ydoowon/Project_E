using System.Collections;
using System.Collections.Generic;
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
    SPlayer Player;

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

}
