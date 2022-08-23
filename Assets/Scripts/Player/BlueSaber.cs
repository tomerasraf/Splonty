using UnityEngine;

public class BlueSaber : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red Shape")) {
            Debug.Log("BLUE Saber HIT the wrong COLOR");
        }

        if (other.CompareTag("Blue Shape")) {
            Debug.Log("BLUE Saber HIT the right COLOR");
        }
    }
}
