using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] ShapeData _shapeData;

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
    
    public event Action onGameOver;
    public void GameOver()
    {
        onGameOver?.Invoke();
    }

    public event Action onShapeHit;
    public void ShapeHit()
    {
        onShapeHit?.Invoke();
    }
    public event Action<int> onBombHit;
    public void BombHit()
    {
        onBombHit?.Invoke(_shapeData.boombShapeDamage);
    }

    public event Action<int> onShieldHit;
    public void ShieldHit() {
        onShieldHit?.Invoke(_shapeData.sheildShapeDamage);
    } 
    
    public event Action<int> onShapeMiss;
    public void ShapeMiss() {
        onShapeMiss?.Invoke(_shapeData.missShapeDamage);
    }
    
    public event Action<int> onWrongShapeHit;
    public void WrongShapeHit() {
        onWrongShapeHit?.Invoke(_shapeData.colorShapeDamage);
    }
}
