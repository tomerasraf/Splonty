using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    static public EventManager current;

    private void Awake()
    {
        current = this;
    }
 
    public event Action onStartGameTouch;
    public void StartGameTouch()
    {
        onStartGameTouch?.Invoke();
    }

    public event Action onEndLevel;
    public void EndLevel()
    {
        onEndLevel?.Invoke();
    }

    public event Action onShapeHit;
    public void ShapeHit()
    {
        onShapeHit?.Invoke();
    }
    public event Action onBombHit;
    public void BombHit()
    {
        onShapeHit?.Invoke();
    }

    public event Action onShieldHit;
    public void ShieldHit() {
        onShieldHit?.Invoke();
    }





}
