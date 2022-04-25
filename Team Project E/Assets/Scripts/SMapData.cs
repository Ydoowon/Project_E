using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map
{
    public Map(int _Mapnum, int _RoomRow, int _RoomCol, string[] line)
    {
        if (_RoomRow * _RoomCol != line.Length) return;

        RoomRow = _RoomRow;
        Roomcol = _RoomCol;
        Mapnum = _Mapnum;

        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            myRooms.Add(new SRoom(row[0] == "TRUE", row[1] == "TRUE", row[2] == "TRUE",
            row[3] == "TRUE", row[4] == "TRUE", row[5] == "TRUE", row[6] == "TRUE"));
        }
    }

    public Map(int _Mapnum, int _RoomRow, int _RoomCol)
    {
        for (int i = 0; i < _RoomRow * _RoomCol; i++)
        {
            myRooms.Add(new SRoom());
        }
        Mapnum = _Mapnum;
        Roomcol = _RoomCol;
        RoomRow = _RoomRow;
    }

    public List<SRoom> myRooms = new List<SRoom>();
    public int Mapnum;
    int RoomRow;
    int Roomcol;
    int Price;

    bool _IsSetting = false;
    public bool IsSetting
    {
        get { return _IsSetting; }
        set { _IsSetting = value; }
    }


    public int GetRoomRow()
    {
        return RoomRow;
    }
    public int GetRoomCol()
    {
        return Roomcol;
    }
    public int GetPrice()
    {
        return Price;
    }
    public void SetPrice(int _Price)
    {
        Price = _Price;
    }

    public void SetRoomsDoor(int _Roomnum, int ImageSetNum)
    {
        if (_Roomnum >= myRooms.Count) return;

        myRooms[_Roomnum].EntDown = ImageSetNum % 2 == 1 ? true : false;
        ImageSetNum = (ImageSetNum >> 1);
        myRooms[_Roomnum].EntUp = ImageSetNum % 2 == 1 ? true : false;
        ImageSetNum = (ImageSetNum >> 1);
        myRooms[_Roomnum].EntLeft = ImageSetNum % 2 == 1 ? true : false;
        ImageSetNum = (ImageSetNum >> 1);
        myRooms[_Roomnum].EntRight = ImageSetNum % 2 == 1 ? true : false;
    }

}

public class SMapData : MonoBehaviour
{
    public List<Map> myMaps;
    public TextAsset Map1;

    void Start()
    {
        string[] line = Map1.text.Substring(0, Map1.text.Length).Split('\n');
        myMaps.Add(new Map(0,3,4,line));
    }


    public int CompareMap(int _mapnum, Map _usermap)
    {
        int score = 0;
        if (myMaps[_mapnum].GetRoomRow() != _usermap.GetRoomRow() || myMaps[_mapnum].GetRoomCol() != _usermap.GetRoomCol()) return 0;
 
        Map OriginMap = myMaps[_mapnum];

        for (int i = 0; i < OriginMap.GetRoomRow() * OriginMap.GetRoomCol(); i++)
        {
            if (!_usermap.myRooms[i].Checking) continue; // 들어가보지 않은 방인 경우, 스코어 계산 스킵


            if(OriginMap.myRooms[i].EntUp == _usermap.myRooms[i].EntUp)
                score++;
            if (OriginMap.myRooms[i].EntRight == _usermap.myRooms[i].EntRight)
                score++;
            if (OriginMap.myRooms[i].EntDown == _usermap.myRooms[i].EntDown)
                score++;
            if (OriginMap.myRooms[i].EntLeft == _usermap.myRooms[i].EntLeft)
                score++;
            if (OriginMap.myRooms[i].IsItem == _usermap.myRooms[i].IsItem)
                score++;
            if (OriginMap.myRooms[i].IsMonster == _usermap.myRooms[i].IsMonster)
                score++;
            if (OriginMap.myRooms[i].IsTrap == _usermap.myRooms[i].IsTrap)
                score++;
        }

        int TotalScore = (int)((float)score / (float)(OriginMap.GetRoomCol() * OriginMap.GetRoomRow() * 7) * (float)100);

        return TotalScore;
    }
}
