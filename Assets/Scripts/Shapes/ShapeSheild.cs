using System.Collections;
using UnityEngine;

public class ShapeSheild : MonoBehaviour
{
    [Header("Shape Sheild Vars")]
    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;

    private Collider[] hitColliders;

    private void Start()
    {
        StartCoroutine(Rotate());
    }

    private void HitDetector()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 3);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Red Saber") || hitCollider.CompareTag("Blue Saber"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void RotateShapeSheildRandomly()
    {
        if (Random.value <= 0.5f)
        {
            rotationSpeed = -rotationSpeed;
        }
        else
        {
            rotationSpeed = Mathf.Abs(rotationSpeed);
        }
    }

    IEnumerator Rotate()
    {
        RotateShapeSheildRandomly();

        while (transform.gameObject != null)
        {
            HitDetector();
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpeed * Time.deltaTime);
            transform.Rotate(transform.rotation.x, rotationSpeed, transform.rotation.z);
            yield return null;
        }

        yield return null;
    }
}
