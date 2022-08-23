using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

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
