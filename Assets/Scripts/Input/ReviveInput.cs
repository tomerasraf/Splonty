using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReviveInput : MonoBehaviour
{
     [SerializeField] GameData _gameData;
     [SerializeField] Button reviveButton;

    private void Start()
    {
        reviveButton.onClick.AddListener(() =>
        {
            Revive();
        });
    }

    private void Revive()
    {
        _gameData.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
