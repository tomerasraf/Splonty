using UnityEngine;
public class PlayerHP : MonoBehaviour
{
    [SerializeField] GameData _gameData;
    private void OnEnable()
    {
        EventManager.current.onWrongColorHit += PlayerGettingHit;
        EventManager.current.onBombHit += PlayerGettingHit;
        EventManager.current.onShieldHit += PlayerGettingHit;
        EventManager.current.onShapeHit += PlayerIncreaseHP;
        EventManager.current.onShapeMiss += PlayerGettingHit;
    }
    private void OnDisable()
    {
        EventManager.current.onWrongColorHit -= PlayerGettingHit;
        EventManager.current.onBombHit -= PlayerGettingHit;
        EventManager.current.onShieldHit -= PlayerGettingHit;
        EventManager.current.onShapeHit -= PlayerIncreaseHP;
        EventManager.current.onShapeMiss -= PlayerGettingHit;
    }

    private void PlayerIncreaseHP()
    {  
        if (_gameData.healthPoints > 95) { return; }
      
        _gameData.healthPoints += 5;
        EventManager.current.UIHealthChange();
    }

    private void PlayerGettingHit(int damage)
    {
       
        if (_gameData.healthPoints <= 0)
        {
            EventManager.current.GameOver();
            return;
        }

        _gameData.healthPoints -= damage;
        EventManager.current.UIHealthChange();
    }
}
