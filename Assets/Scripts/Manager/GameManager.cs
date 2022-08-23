using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] ShapeData _shapeData;
    [SerializeField] GameData _gameData;

    [SerializeField] GameObject level1ParentObject;
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
    }

    #region EventCallers
    private void OnEnable()
    {
        EventManager.current.onStartGameTouch += StartGame;
        EventManager.current.onWrongShapeHit += PlayerGettingHit;
        EventManager.current.onBombHit += PlayerGettingHit;
        EventManager.current.onShieldHit += PlayerGettingHit;
        EventManager.current.onShapeHit += PlayerIncreaseHP;
    }

    private void PlayerIncreaseHP() {
        if (_gameData.healthPoints > 95) { return; }

        _gameData.healthPoints += 5;
    }

    private void PlayerGettingHit (int damage)
    {
        if (_gameData.healthPoints <= 0) {
            EventManager.current.GameOver();
            return;
        }

        _gameData.healthPoints -= damage;
    }

    private void OnDisable()
    {
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

    #endregion
}
