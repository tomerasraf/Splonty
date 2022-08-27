using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] GameData _gameData; 

    private void OnEnable()
    {
        EventManager.current.onPlayerGetScore += AddScore;
    }

    private void AddScore(int shapePointsAdded)
    {
       _gameData.score += shapePointsAdded;
        EventManager.current.ScoreUpdate();
    }


}
