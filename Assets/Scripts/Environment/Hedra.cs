using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Hedra : MonoBehaviour
{
    [Header("Headras")]
    [SerializeField] GameObject hedras;

    [Header("Spawners")]
    [SerializeField] Transform[] sideSpawners;

    [Header("Parent")]
    [SerializeField] Transform hedraParent;

    [Header("Colors")]
    [SerializeField]
    Color[] colors;

    [Header("Vars")]
    [SerializeField] float dealyBetweenSpawn;

    private void Start()
    {
        StartCoroutine(SpwanHedras());
    }

    IEnumerator SpwanHedras()
    {
        while (true)
        {
          GameObject hedra = Instantiate(
           hedras,
           sideSpawners[Random.Range(0, 2)].position,
           Quaternion.Euler(90,0,0),
           hedraParent);

            BoxCollider box = hedra.AddComponent<BoxCollider>();
            box.isTrigger = enabled;

            Color hedraRandomColor = hedra.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, 5)];
            hedra.GetComponent<MeshRenderer>().material.EnableKeyword ("_EMISSION");
            hedra.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", hedraRandomColor * 0.7f);


            //hedra.transform.DORotate(new Vector3(transform.rotation.x, transform.rotation.y, 180), 3).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
            hedra.transform.DOMoveZ(transform.forward.z, 3).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);

            yield return new WaitForSeconds(dealyBetweenSpawn);

            yield return null;
        }
    }

}