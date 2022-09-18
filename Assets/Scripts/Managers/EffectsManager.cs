using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [SerializeField] GameObject blueExplostion;
    [SerializeField] GameObject redExplostion;
    [SerializeField] GameObject bombExplotion;
    [SerializeField] GameObject sheildShapeExplostion;

    public static void effectSpawner(GameObject effect, Collider hit) {
        Instantiate(effect, hit.transform.position, Quaternion.identity);
    }
}
