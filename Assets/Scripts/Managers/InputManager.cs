using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    [SerializeField] Canvas menuCanvas;
    [SerializeField] TextMeshProUGUI levelTitle;
    [SerializeField] GameData gameData;
    [SerializeField] AudioClip musicClip;

    private void Update()
    {
        DitectTouch();
    }

    private void Start()
    {
        MoonSDK.TrackLevelEvents(MoonSDK.LevelEvents.Start, gameData.Level);
        levelTitle.text = $"Level {gameData.Level.ToString()}"; 
    }

    private void DitectTouch()
    {
        if (menuCanvas.isActiveAndEnabled)
        {
            if (Input.touchCount > 0)
            {
                SoundManager.Instance.playMusic(musicClip);
                EventManager.current.StartGameTouch();
            }
        }
    }
}
