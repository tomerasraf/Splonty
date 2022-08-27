using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground")) {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Red Shape") || other.CompareTag("Blue Shape") || other.CompareTag("Sheild Shape")) {
            EventManager.current.ShapeMiss();
            EventManager.current.Feedback(0);
        }
    }
}
