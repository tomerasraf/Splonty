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

    [Header("Refereces")]
    [SerializeField] EditorManager _editorManager; 

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
        if (_editorManager.onSlot) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
        float mouseYPos = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(0) && mouseYPos != 0 && Input.GetKey(KeyCode.LeftControl)) {

            Debug.Log(mouseYPos + " Mouse Y Pos");
           

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Debug.Log(pos.y + " Mouse Y Pos with screen viewport");
            Vector3 move = new Vector3(0, 0, pos.y * dragSpeed);

            transform.Translate(-move, Space.World);
            Debug.Log(move + "Move vector");
        };



       
    }
}
