using UnityEngine;

public class BlueSaberCollision : MonoBehaviour
{
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
            Destroy(Instantiate(blueExplostion, other.transform.position, Quaternion.identity), 2);
            Destroy(other.gameObject);
            EventManager.current.ShapeHit();
            EventManager.current.PlayerGetScore(_scoreData.colorShapePoints);
        }
    }
}
