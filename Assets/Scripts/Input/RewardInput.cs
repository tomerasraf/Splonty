using UnityEngine;
using UnityEngine.UI;

public class RewardInput : MonoBehaviour
{
    [SerializeField] Button rewardButton;

    private void OnEnable()
    {
        rewardButton.onClick.AddListener(() => {
           // EventManager.current.OpenRewardedAd();
            AdManager.instance.ShowRewardAd();
        });
    }
}
