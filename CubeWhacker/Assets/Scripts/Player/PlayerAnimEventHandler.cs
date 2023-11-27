using System;
using UnityEngine;

/// <summary>
/// Catches events from the player's animation and passes them on 
/// </summary>
public class PlayerAnimEventHandler : MonoBehaviour
{
    public event Action OnThrowReleaseEvent;
    public event Action OnThrowEndEvent;
    public event Action OnAttackEndEvent;
    public event Action OnSwingStartEvent;

    public void HandleSwingStartEvent()
    {
        OnSwingStartEvent?.Invoke();
    }

    public void HandleThrowReleaseEvent()
    {
        OnThrowReleaseEvent?.Invoke();
    }

    public void HandleThrowEndEvent()
    {
        OnThrowEndEvent?.Invoke();
    }

    public void HandleAttackEndEvent()
    {
        OnAttackEndEvent?.Invoke();
    }
}