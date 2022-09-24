using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerCollision : MonoBehaviour
{
    [SerializeField] AudioClip missCLip;
    [SerializeField] AudioClip comboBreaker;
    [SerializeField] GameData _gameData;
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground") || !other.CompareTag("End Level")) {
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Red Shape") || other.CompareTag("Blue Shape") || other.CompareTag("Sheild Shape")) {
            if (_gameData.comboHits > 20)
            {
                SoundManager.Instance.PlayOneShotSound(comboBreaker);
                EventManager.current.ShapeMiss();
                EventManager.current.Feedback(0);
            }
            else
            {
                SoundManager.Instance.PlayMissChocker(missCLip);
                EventManager.current.ShapeMiss();
                EventManager.current.Feedback(0);
            }
        }
    }
}
