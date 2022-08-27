using UnityEngine;

public class SaberCollision : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clip;

    [Header("Data")]
    [SerializeField] ScoreData _scoreData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield")) {

            int random = Random.Range(0, 3);
            SoundManager.Instance.PlaySound(_clip[random]);

            EventManager.current.ShapeHit();
            EventManager.current.ShieldHit();
            EventManager.current.PlayerGetScore(_scoreData.sheildShapePoints);
            EventManager.current.UIHealthChange();
        }

        if (other.CompareTag("Boomb Shape")) {
            int random = Random.Range(0, 3);
            SoundManager.Instance.PlaySound(_clip[random]);

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
