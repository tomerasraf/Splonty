using UnityEngine;

public class RedSaberCollision : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] ScoreData _scoreData;

    [Header("Effects")]
    [SerializeField] GameObject redExplostion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red Shape"))
        {
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
    }

}
