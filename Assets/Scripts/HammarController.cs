using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammarController : MonoBehaviour
{

    [Header("Hammar Vars")]
    [SerializeField] float hammarSpeed;

    private float sceneWidth;
    private Touch touch;
    private Vector3 startTouchPosion;
    private Vector3 currentTouchPosition;
    private Quaternion startRotation;

    private void Start()
    {
        sceneWidth = Screen.width;
    }

    private void Update()
    {
        SlideToRotate();
    }

    private void SlideToRotate() {

        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                startTouchPosion = touch.position;
                startRotation = transform.rotation;
               
            }

            if (touch.phase == TouchPhase.Moved)
            {
                currentTouchPosition = touch.position;
                Debug.Log(currentTouchPosition + "Current Pos");

                float magnitude = currentTouchPosition.x - startTouchPosion.x;
                transform.rotation = startRotation * Quaternion.Euler( Vector3.up * (magnitude * hammarSpeed / sceneWidth) * 360);
     
            }
        }
    } 
}
