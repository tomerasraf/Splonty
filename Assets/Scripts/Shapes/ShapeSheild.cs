using System.Collections;
using UnityEngine;

public class ShapeSheild : MonoBehaviour
{
    [Header("Shape Sheild Vars")]
    [SerializeField] float rotationSpeed;
    [SerializeField] float moveSpeed;

    private void Start()
    {
        StartCoroutine(Rotate());
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
            transform.Rotate(transform.rotation.x, rotationSpeed, transform.rotation.z);
            yield return null;
        }

        yield return null;
    }
}
