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
    [SerializeField] float timeScale;

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
        StartCoroutine(SetTimeScale());

        // Ad Requests
        AdManager.instance.RequestRewardAd();
    }

    private void GameOver()
    {
        StopAllCoroutines();
    }

    private void EndLevel()
    {
        //AdManager.instance.ShowInterstitial();    
        AdManager.instance.ShowRewardAd();
    }

    #region EventCallers
    private void OnEnable()
    {
        EventManager.current.onStartGameTouch += StartGame;
        EventManager.current.onEndLevel += EndLevel;
        EventManager.current.onGameOver += GameOver;
    }
    private void OnDisable()
    {
        EventManager.current.onGameOver -= GameOver;
        EventManager.current.onEndLevel -= EndLevel;
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
        _gameData.fullLevelDistance = (endLevelCollider.position - lightSaber.position).sqrMagnitude;

        while (true)
        {
            if (endLevelCollider != null)
            {
                _gameData.currentLevelProgress = (endLevelCollider.position - lightSaber.position).sqrMagnitude;
            }

            yield return null;
        }
    }

    IEnumerator SetTimeScale()
    {

        while (true)
        {

            Time.timeScale = timeScale;
            yield return null;
        }
    }

    #endregion
}
