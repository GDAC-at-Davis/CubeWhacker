using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic component that is checked for by any attack. Should be placed on the same gameObject as the hitbox
/// </summary>
public class CombatEntity : MonoBehaviour
{
    public event Action OnHit;
    
    public void Hit()
    {
        OnHit?.Invoke();
    }
}
