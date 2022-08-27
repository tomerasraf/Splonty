using UnityEngine;
using DG.Tweening;
using System.Collections;

public class shapeMovement : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] float animationDuration;

    private void Start()
    {
        StartCoroutine(ShapeMoveFromSideToSide());   
    }

    IEnumerator ShapeMoveFromSideToSide() {

        transform.DOMoveX(offset, animationDuration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

        yield return null;
    }
}