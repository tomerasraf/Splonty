using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] GameData _gameData;

    [Header("Objects")]
    [SerializeField] GameObject level1ParentObject;

    [Header("Transforms")]
    [SerializeField] Transform lightSaber;
    [SerializeField] Transform endLevelCollider;

    [Header("Vars")]
    [SerializeField] float levelSpeed;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }

    private void StartGame()
    {
        StartCoroutine(MoveLevel());
        StartCoroutine(CountGameTime());
        StartCoroutine(LevelProgress());
    }

    private void GameOver()
    {
        StopAllCoroutines();
    }

    #region EventCallers
    private void OnEnable()
    {
        EventManager.current.onStartGameTouch += StartGame;
        EventManager.current.onGameOver += GameOver;
    }
    private void OnDisable()
    {
        EventManager.current.onGameOver -= GameOver;
        EventManager.current.onStartGameTouch -= StartGame;
    }

    #endregion

    #region IEnumerators
    IEnumerator CountGameTime()
    {
        while (true)
        {
            _gameData.gameplayTime += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator MoveLevel()
    {
        while (true)
        {
            level1ParentObject.transform.position = new Vector3(
                level1ParentObject.transform.position.x,
                level1ParentObject.transform.position.y,
                level1ParentObject.transform.position.z - levelSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator LevelProgress()
    {
        while (true)
        {
            _gameData.levelProgress = ((endLevelCollider.position.z - lightSaber.position.z) * (-1)) / 10;

            yield return null;
        }
    }

    #endregion
}
