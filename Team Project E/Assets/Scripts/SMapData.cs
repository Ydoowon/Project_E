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
    public TextAsset[] MapList;

    void Start()
    {
        for(int i = 0; i < MapList.Length; i++)
        {
            string[] line = MapList[i].text.Substring(0, MapList[i].text.Length).Split('\n');
            myMaps.Add(new Map(i, 3, 4, line));
        }
    }


    public int CompareMap(int _mapnum, Map _usermap)
    {
        int score = 0;
        if (myMaps[_mapnum].GetRoomRow() != _usermap.GetRoomRow() || myMaps[_mapnum].GetRoomCol() != _usermap.GetRoomCol()) return 0;
 
        Map OriginMap = myMaps[_mapnum];

        for (int i = 0; i < OriginMap.GetRoomRow() * OriginMap.GetRoomCol(); i++)
        {
            if (!_usermap.myRooms[i].Checking) continue; // 들어가보지 않은 방인 경우, 스코어 계산 스킵

            // 모든 문의 위치가 일치할 경우
            if(OriginMap.myRooms[i].EntUp == _usermap.myRooms[i].EntUp 
                && OriginMap.myRooms[i].EntRight == _usermap.myRooms[i].EntRight
                && OriginMap.myRooms[i].EntDown == _usermap.myRooms[i].EntDown
                && OriginMap.myRooms[i].EntLeft == _usermap.myRooms[i].EntLeft)
            {
                score += 4;
            }

                
            if (OriginMap.myRooms[i].IsItem == _usermap.myRooms[i].IsItem)
                score++;
            if (OriginMap.myRooms[i].IsMonster == _usermap.myRooms[i].IsMonster)
                score++;
            if (OriginMap.myRooms[i].IsTrap == _usermap.myRooms[i].IsTrap)
                score++;
        }

        int TotalScore = (int)((float)score / (float)(OriginMap.GetRoomCol() * OriginMap.GetRoomRow() * 7) * 100);
        switch(_usermap.Mapnum)
        {
            case 0:
                TotalScore *= Random.Range(40, 50);
                break;
            case 1:
                TotalScore *= Random.Range(50, 60);
                break;
            case 2:
                TotalScore *= Random.Range(60, 70);
                break;
        }
        return TotalScore;
    }
}
