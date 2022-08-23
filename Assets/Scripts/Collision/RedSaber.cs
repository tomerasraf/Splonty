using UnityEngine;

public class RedSaber : MonoBehaviour
{
    [SerializeField] GameObject redExplostion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red Shape"))
        {
            Destroy(Instantiate(redExplostion, other.transform.position, Quaternion.identity), 2);
            Destroy(other.gameObject);
            EventManager.current.ShapeHit();
        }

        if (other.CompareTag("Blue Shape"))
        {
            EventManager.current.WrongShapeHit();
        }
    }

}
