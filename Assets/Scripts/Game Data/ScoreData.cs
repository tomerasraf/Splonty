using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Score Data", menuName = "Data/Score Data", order = 0)]
public class ScoreData : ScriptableObject   
{
    [Header("Score Data")]
    public int colorShapePoints;
    public int sheildShapePoints;
    public int boombShapePoints;
}

