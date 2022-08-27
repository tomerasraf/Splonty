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

    #region Player HP Events
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
    public void ShieldHit()
    {
        onShieldHit?.Invoke(_shapeData.sheildShapeDamage);
    }

    public event Action<int> onShapeMiss;
    public void ShapeMiss()
    {
        onShapeMiss?.Invoke(_shapeData.missShapeDamage);
    }

    public event Action<int> onWrongColorHit;
    public void WrongShapeHit()
    {
        onWrongColorHit?.Invoke(_shapeData.colorShapeDamage);
    }
    #endregion

    #region Player Score/Combo Events

    public event Action<int> onPlayerGetScore;
    public void PlayerGetScore(int score)
    {
        onPlayerGetScore?.Invoke(score);
    }
    #endregion

    #region UI Events
    public event Action onUIHealthChange;
    public void UIHealthChange()
    {
        onUIHealthChange?.Invoke();
    }

    public event Action onScoreUpdate;
    public void ScoreUpdate()
    {
        onScoreUpdate?.Invoke();
    }

    public event Action<int> onFeedback;
    public void Feedback(int feedbackIndex)
    {
        onFeedback?.Invoke(feedbackIndex);
    }

    public event Action onComboUIUpdater;
    public void ComboUIUpdater()
    {
        onComboUIUpdater?.Invoke();
    }

    #endregion

    #region Game Events
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
    #endregion

    #region Sound Events

    #endregion
}
