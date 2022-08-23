using DG.Tweening;
using System.Collections;
using UnityEngine;


public class NgonShape : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Rotate());
    }
    IEnumerator Rotate()
    {
        Quaternion randomRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Random.rotation.z);
        transform.rotation = randomRotation;

        transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, 180), 3).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
        yield return null;
    }
}
