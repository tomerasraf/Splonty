using UnityEngine;

public class BlueSaberCollision : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clip;

    [Header("Data")]
    [SerializeField] ScoreData _scoreData;

    [Header("Effects")]
    [SerializeField] GameObject blueExplostion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red Shape"))
        {
            EventManager.current.WrongShapeHit();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Blue Shape"))
        {
            int random = Random.Range(0, 3);
            SoundManager.Instance.PlaySound(_clip[random]);
            Destroy(Instantiate(blueExplostion, other.transform.position, Quaternion.identity), 2);
            Destroy(other.gameObject);
            EventManager.current.ShapeHit();
            EventManager.current.PlayerGetScore(_scoreData.colorShapePoints);
        }
    }
}
