using UnityEngine;

public class RedSaberCollision : MonoBehaviour
{

    [SerializeField] private AudioClip[] _clip;

    [Header("Data")]
    [SerializeField] ScoreData _scoreData;

    [Header("Effects")]
    [SerializeField] GameObject redExplostion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red Shape"))
        {
            int random = Random.Range(0, 3);
            SoundManager.Instance.PlayOneShotSound(_clip[random]);
            Destroy(Instantiate(redExplostion, other.transform.position, Quaternion.identity), 2);
            Destroy(other.gameObject);
            EventManager.current.ShapeHit();
            EventManager.current.PlayerGetScore(_scoreData.colorShapePoints);
        }

        if (other.CompareTag("Blue Shape"))
        {
            EventManager.current.WrongShapeHit();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Parallel Red Shape"))
        {
            int random = Random.Range(0, 3);
            SoundManager.Instance.PlaySound(_clip[random]);
            Destroy(Instantiate(redExplostion, other.transform.position, Quaternion.identity), 2);
            Destroy(other.gameObject);
            EventManager.current.ShapeHit();
            EventManager.current.PlayerGetScore(_scoreData.colorShapePoints);
        }
    }

}
