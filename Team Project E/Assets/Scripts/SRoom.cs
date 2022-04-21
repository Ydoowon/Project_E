using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SRoom
{
    public SRoom(bool _Entup, bool _Entright, bool _Entdown, bool _Entleft, bool _Item, bool _Monster, bool _Trap)
    {
        Entup = _Entup;
        Entright = _Entright;
        Entdown = _Entdown;
        Entleft = _Entleft;
        Item = _Item;
        Monster = _Monster;
        Trap = _Trap;
    }
    public SRoom() { }

    [SerializeField]
    bool Entup = false;  
    public bool EntUp
    {
        get { return Entup; }
        set { Entup = value; }
    }
    [SerializeField]
    bool Entright = false;
    public bool EntRight
    {
        get { return Entright; }
        set { Entright = value; }
    }
    [SerializeField]
    bool Entdown = false;  
    public bool EntDown
    {
        get { return Entdown; }
        set { Entdown = value; }
    }
    [SerializeField]
    bool Entleft = false;  
    public bool EntLeft
    {
        get { return Entleft; }
        set { Entleft = value; }
    }

    [SerializeField]
    bool Item = false;
    public bool IsItem
    {
        get { return Item; }
        set { Item = value; }
    }
    [SerializeField]
    bool Monster = false;
    public bool IsMonster
    {
        get { return Monster; }
        set { Monster = value; }
    }
    [SerializeField]
    bool Trap = false;
    public bool IsTrap
    {
        get { return Trap; }
        set { Trap = value; }
    }
   
}
