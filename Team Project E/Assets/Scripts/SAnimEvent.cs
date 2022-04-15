using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SAnimEvent : MonoBehaviour
{
    public event UnityAction StandUp = null;
    public event UnityAction Attack = null;
    public void OnStandUp()
    {
        StandUp?.Invoke();
    }
    public void OnAttack()
    {
        Attack?.Invoke();
    }    
    
}
