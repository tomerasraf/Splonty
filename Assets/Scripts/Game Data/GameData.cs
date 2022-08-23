using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Game Data", menuName = "Data", order = 0)]
public class GameData : ScriptableObject   
{
    [Header("Player Data")]
    public int healthPoints = 0;
    public int coins = 0;
    public int score = 0;
    public int highestScore = 0;
    public int comboHits = 0;
    public int highestCombo = 0;
    public float gameplayTime = 0;
}

