using UnityEngine;

public class SaberController : MonoBehaviour
{

    [Header("Saber Vars")]
    [SerializeField] float saberSpeed;
    [SerializeField] float smoothRotation;
    [SerializeField] LayerMask layer;

    private void Update()
    {
        PointToRotate();
    }

    #region SaberControl

    private void PointToRotate()
    {
        if (Input.touchCount > 0)
        {
            // Geting the position of the finger on the screen.
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosFar = new Vector3(touch.position.x, touch.position.y, Camera.main.farClipPlane);
            Vector3 touchPosNear = new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane);
            Vector3 touchPosF = Camera.main.ScreenToWorldPoint(touchPosFar);
            Vector3 touchPosN = Camera.main.ScreenToWorldPoint(touchPosNear);

            // Info about the hit object.
            RaycastHit hit;

            // Shooting a ray from the camera to the desire player.
            if (Physics.Raycast(touchPosN, touchPosF - touchPosN, out hit, 100, layer))
            {
                GrabHandle(hit);
            }

          /*  if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                rb.AddTorque(0, 100, 0, ForceMode.Impulse);
            }
          */
        }
    }

    void GrabHandle(RaycastHit hit)
    {
        if (hit.transform.name == "Ground")
        {
            Vector3 newHitPoint = new Vector3(hit.point.x, 0, hit.point.z);

            Vector3 newDiraction = Vector3.RotateTowards(transform.forward, newHitPoint, saberSpeed * Time.deltaTime, 0.0f);
            //Destroy(Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), hit.point, Quaternion.identity), 2f);
            Debug.DrawRay(transform.position, newDiraction, Color.red);

            transform.rotation = Quaternion.LookRotation(newDiraction);
        }
    }
    #endregion
}



