using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SAnimEvent : MonoBehaviour
{
    public event UnityAction StandUp = null;
    public event UnityAction Attack = null;
    public event UnityAction Move = null;
    public void OnStandUp()
    {
        StandUp?.Invoke();
    }
    public void OnAttack()
    {
        Attack?.Invoke();
    }    
    public void DoMove()
    {
        Move?.Invoke();
    }
    
}
