using UnityEngine;

public class BlueSaber : MonoBehaviour
{
    [SerializeField] GameObject blueExplostion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red Shape"))
        {
            EventManager.current.WrongShapeHit();
        }

        if (other.CompareTag("Blue Shape"))
        {
            Destroy(Instantiate(blueExplostion, other.transform.position, Quaternion.identity), 2);
            Destroy(other.gameObject);
            EventManager.current.ShapeHit();
        }
    }
}
