using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerCollision : MonoBehaviour
{
    [SerializeField] AudioClip missCLip;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground") || !other.CompareTag("End Level")) {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Red Shape") || other.CompareTag("Blue Shape") || other.CompareTag("Sheild Shape")) {
            SoundManager.Instance.PlaySound(missCLip);
            EventManager.current.ShapeMiss();
            EventManager.current.Feedback(0);
        }
    }
}
