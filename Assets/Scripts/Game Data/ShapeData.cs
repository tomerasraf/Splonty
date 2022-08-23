using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Shape Data", menuName = "Data/Shapes", order = 0)]
public class ShapeData : ScriptableObject   
{
    [Header("Shape Damage Data")]
    public int colorShapeDamage;
    public int sheildShapeDamage;
    public int boombShapeDamage;
    public int missShapeDamage;
}

