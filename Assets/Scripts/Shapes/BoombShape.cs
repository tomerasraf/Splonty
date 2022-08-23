using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BoombShape : MonoBehaviour
{
    [SerializeField] GameObject explotionEffect;

    private void SaberCollision_OnBoombExplode()
    {
        Debug.Log("Boom!!");
    }
}
