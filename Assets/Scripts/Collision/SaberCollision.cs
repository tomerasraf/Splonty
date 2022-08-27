using System;
using System.Collections.Generic;
using UnityEngine;

public class SaberCollision : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] ScoreData _scoreData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield")) {
            EventManager.current.ShapeHit();
            EventManager.current.ShieldHit();
            EventManager.current.PlayerGetScore(_scoreData.sheildShapePoints);
            EventManager.current.UIHealthChange();
        }

        if (other.CompareTag("Boomb Shape")) {
            Destroy(other.gameObject);
            EventManager.current.ShapeHit();
            EventManager.current.BombHit();
            EventManager.current.UIHealthChange();
        }

        if (other.CompareTag("End Level")) {
            EventManager.current.EndLevel();
        }
    }
}
