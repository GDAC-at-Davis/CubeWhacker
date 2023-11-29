using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic component that is checked for by any attack. Should be placed on a parent gameobject to the hitbox collider
/// </summary>
public class CombatEntity : MonoBehaviour
{
    public event Action OnHit;
    
    public void Hit()
    {
        OnHit?.Invoke();
    }
}
