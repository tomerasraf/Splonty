using DG.Tweening;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Camera Shake")]
    [SerializeField] float shakeDuration;
    [SerializeField] float shakeStrength;
    [SerializeField] int shakeViberation;
    [SerializeField] float randomness;

    private void OnEnable()
    {
        EventManager.current.onWrongColorHit += ShakeCamera;
    }

    private void ShakeCamera()
    {
       StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Camera.main.DOShakeRotation(shakeDuration, shakeStrength, shakeViberation, 90).OnComplete(() => {
            Camera.main.transform.rotation = Quaternion.Euler(35.289f, 0, 0);
        }); 
        yield return null;
    }
}
