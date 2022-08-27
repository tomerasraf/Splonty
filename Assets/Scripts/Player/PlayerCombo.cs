using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] GameData _gameData;

    private void OnEnable()
    {
        EventManager.current.onShapeHit += ComboCounter;
        EventManager.current.onWrongColorHit += ResetCombo;
        EventManager.current.onShapeMiss += ResetCombo;
        EventManager.current.onBombHit += ResetCombo;
        EventManager.current.onShieldHit += ResetCombo;
    }
    private void OnDisable()
    {
        EventManager.current.onShapeHit -= ComboCounter;
        EventManager.current.onWrongColorHit -= ResetCombo;
        EventManager.current.onShapeMiss -= ResetCombo;
        EventManager.current.onBombHit -= ResetCombo;
        EventManager.current.onShieldHit -= ResetCombo;
    }

    private void ResetCombo(int obj)
    {
        _gameData.comboHits = 0;
    }

    private void ComboCounter()
    {
        _gameData.comboHits++;

        if (_gameData.comboHits > _gameData.highestCombo) {
            _gameData.highestCombo++;
        }

        EventManager.current.ComboUIUpdater();
    }
}