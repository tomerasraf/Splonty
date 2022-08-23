using System.Collections;
using UnityEngine;

public class BlueShape : MonoBehaviour
{
    [SerializeField] float blueShapeSpeed;
    [SerializeField] GameObject blueExplostion;
    
    private Collider[] hitColliders;
   
    private void Update()
    {
        HitDetector("Blue Saber");
    }

    private void HitDetector(string tag)
    {
        hitColliders = Physics.OverlapSphere(transform.position, 3);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(tag))
            {

                Destroy(Instantiate(blueExplostion, hitCollider.transform.position, Quaternion.identity), 2);
                Destroy(gameObject);
                EventManager.current.ShapeHit();
            }
            else if (hitCollider.CompareTag("Red Saber"))
            {
              
            }
        }
    }
}
