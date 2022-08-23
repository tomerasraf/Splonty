using UnityEngine;


public class InputManager : MonoBehaviour
{
    [SerializeField] Canvas menuCanvas;

    private void Update()
    {
        DitectTouch();
    }

    private void DitectTouch()
    {
        if (menuCanvas.isActiveAndEnabled)
        {
            if (Input.touchCount > 0)
            {
                EventManager.current.StartGameTouch();
            }
        }
    }
}
