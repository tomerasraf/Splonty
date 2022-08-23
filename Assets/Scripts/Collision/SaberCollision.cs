using System;
using System.Collections.Generic;
using UnityEngine;

public class SaberCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield")) {
            EventManager.current.ShieldHit();
        }

        if (other.CompareTag("Boomb Shape")) {
            Destroy(other.gameObject);
            EventManager.current.BombHit();
        }

        if (other.CompareTag("End Level")) {
            EventManager.current.EndLevel();
        }
    }
}
