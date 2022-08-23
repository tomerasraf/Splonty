using System.Collections;
using UnityEngine;

public class ShapesSpawner : MonoBehaviour
{
    [Header("Shapes Vars")]
    [SerializeField] float shapeZAxisOffset;
    [SerializeField] float shapeMinXAxieOffset;
    [SerializeField] float shapeMaxXAxieOffset;
    [SerializeField] float timeBetweenEachSpawn;
    [SerializeField] float spawnDealy;

    [Header("Shape Prefab")]
    [SerializeField] GameObject redShape;
    [SerializeField] GameObject blueShape;
    [SerializeField] GameObject shapeSheild;
    [SerializeField] GameObject shapeBoomb;

    private Transform saberTranform;

    private void Start()
    {
        saberTranform = FindObjectOfType<SaberController>().transform;
    }

    private void OnEnable()
    {
        EventManager.current.onStartGameTouch += SpawnShape;
    }

    private void OnDisable()
    {
        EventManager.current.onStartGameTouch -= SpawnShape;
    }

    private void SpawnShape()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDealy);

        while (true)
        {
            float randomnShapeXAxisPosition = Random.Range(shapeMinXAxieOffset, shapeMaxXAxieOffset);
            Vector3 spawnPosition = new Vector3(randomnShapeXAxisPosition, saberTranform.position.y, shapeZAxisOffset);

            if (Random.value <= 0.5f)
            {
                Instantiate(redShape, spawnPosition, Quaternion.identity);
            }
            else
            {
                Instantiate(blueShape, spawnPosition, Quaternion.identity);
            }


            Quaternion randomRotation = Quaternion.Euler(Quaternion.identity.x, Random.rotation.y, Quaternion.identity.z);

            if (Random.value <= 0.5f)
            {
                yield return new WaitForSeconds(0.4f);
                if (Random.value <= 0.5f)
                {
                    Instantiate(shapeSheild, new Vector3(7, saberTranform.position.y, shapeZAxisOffset), randomRotation);
                }
                else
                {
                    Instantiate(shapeSheild, new Vector3(-7, saberTranform.position.y, shapeZAxisOffset), randomRotation);
                }
            }
            else if (Random.value <= 0.5f)
            {
                if (Random.value <= 0.5f)
                {
                    Instantiate(shapeBoomb, new Vector3(6.5f, saberTranform.position.y, shapeZAxisOffset), randomRotation);
                }
                else
                {
                    Instantiate(shapeBoomb, new Vector3(-6.5f, saberTranform.position.y, shapeZAxisOffset), randomRotation);
                }
            }
            yield return new WaitForSeconds(timeBetweenEachSpawn);
            yield return null;
        }
    }
}
