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
    [SerializeField]
    bool Entup;  
    public bool EntUp
    {
        get { return Entup; }
        set { Entup = value; }
    }
    [SerializeField]
    bool Entright;
    public bool EntRight
    {
        get { return Entright; }
        set { Entright = value; }
    }
    [SerializeField]
    bool Entdown;  
    public bool EntDown
    {
        get { return Entdown; }
        set { Entdown = value; }
    }
    [SerializeField]
    bool Entleft;  
    public bool EntLeft
    {
        get { return Entleft; }
        set { Entleft = value; }
    }

    [SerializeField]
    bool Item;
    public bool IsItem
    {
        get { return Item; }
        set { Item = value; }
    }
    [SerializeField]
    bool Monster;
    public bool IsMonster
    {
        get { return Monster; }
        set { Monster = value; }
    }
    [SerializeField]
    bool Trap;
    public bool IsTrap
    {
        get { return Trap; }
        set { Trap = value; }
    }
   
}
