using UnityEngine;

public class RedSaber : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red Shape"))
        {
            Debug.Log("RED Saber HIT the right COLOR");
        }

        if (other.CompareTag("Blue Shape"))
        {
            Debug.Log("RED Saber HIT the wrong COLOR");
        }
    }

}
