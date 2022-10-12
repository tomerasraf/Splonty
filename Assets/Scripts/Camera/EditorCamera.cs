using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCamera : MonoBehaviour
{
    [Header("Camera Control")]
    [SerializeField] float dragSpeed = 2;
    [SerializeField] float minFov = 15f;
    [SerializeField] float maxFov = 90f;
    [SerializeField] float sensitivity = 10f;

    private Vector3 dragOrigin;


    void Update()
    {
        ZoomControl();
        DragCamera();
    }

    void ZoomControl() {
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    } 

     void DragCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        transform.Translate(move, Space.World);
    }
}
