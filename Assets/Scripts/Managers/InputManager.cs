using UnityEngine;
using TMPro;


public class InputManager : MonoBehaviour
{
    [SerializeField] Canvas menuCanvas;
    [SerializeField] TextMeshProUGUI levelTitle;
    [SerializeField] GameData gameData;

    private void Update()
    {
        DitectTouch();
    }

    private void Start()
    {
        gameData.Level += 1;
       // MoonSDK.TrackLevelEvents(MoonSDK.LevelEvents.Start, gameData.Level);
        levelTitle.text = $"Level {gameData.Level.ToString()}"; 
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
