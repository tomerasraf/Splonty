using System;
using UnityEngine;


public class RedShape : MonoBehaviour
{
    private Collider[] hitColliders;
    [SerializeField] float redShapeSpeed;
    [SerializeField] GameObject redExplostion;

    private void Update()
    {
        HitDetector("Red Saber");
    }

    private void HitDetector(string tag)
    {
        hitColliders = Physics.OverlapSphere(transform.position, 3);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(tag))
            {
                Destroy(Instantiate(redExplostion, hitCollider.transform.position, Quaternion.identity), 2);
                Destroy(gameObject);
                EventManager.current.ShapeHit();
               
            }
            else if(hitCollider.CompareTag("Blue Saber"))
            {
                
            }
        }
    }
}
