using UnityEngine;

public class SaberController : MonoBehaviour
{

    [Header("Hammer Vars")]
    [SerializeField] float saberSpeed;
    [SerializeField] float smoothRotation;

    private float screenWidth;
    Touch touch;
    Vector3 startTouchPosition;
    Vector3 currentTouchPosition;
    Quaternion startRotation;
    Quaternion endRotation;
    float Xmagnitud;

    private Camera myCam;
    [SerializeField] float angleInDegrees;
    Vector3 saberDiraction;
    Vector3 touchPos;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        myCam = Camera.main;
        screenWidth = Screen.width;
    }

    private void Update()
    {
        PointToRotate();
    }

    #region SaberControl
    private void RotateSaber()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = Input.GetTouch(0).position;
                startRotation = transform.rotation;
            }

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                float currentDistanceBetweenTouchPositions = ((Vector3)touch.position - startTouchPosition).x;
                transform.rotation = startRotation * Quaternion.Euler(Vector3.up * (currentDistanceBetweenTouchPositions / screenWidth) * 360);
            }
        }
    }

    private void DragToRotate()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = myCam.ScreenToViewportPoint(touch.position);
            }

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                Vector3 direction = startTouchPosition - myCam.ScreenToViewportPoint(touch.position);

                angleInDegrees = direction.x * saberSpeed * 180 * Time.deltaTime;

                transform.RotateAround(transform.position, new Vector3(0, -1, 0), angleInDegrees);

                Debug.Log(touch.deltaPosition);
            }
        }
    }

    private void SlideToRotate()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                startRotation = transform.localRotation;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                currentTouchPosition = touch.position;
                Xmagnitud = currentTouchPosition.x - startTouchPosition.x;

                endRotation = Quaternion.Euler(startRotation.x, Xmagnitud * saberSpeed, startRotation.z);

                transform.rotation = Quaternion.Lerp(startRotation, endRotation, smoothRotation);
            }
        }
    }

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
            if (Physics.Raycast(touchPosN, touchPosF - touchPosN, out hit))
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



