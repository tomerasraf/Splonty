using UnityEngine;
using System.Collections;

public class RedSaberCollision : MonoBehaviour
{

    [SerializeField] private AudioClip[] _clip;

    [Header("Data")]
    [SerializeField] ScoreData _scoreData;

    [Header("Effects")]
    [SerializeField] GameObject redExplostion;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Red Shape"))
        {
            int random = Random.Range(0, 3);
            SoundManager.Instance.PlayOneShotSound(_clip[random]);
            
            Destroy(other.gameObject);
            Destroy(Instantiate(redExplostion, other.transform.position, Quaternion.identity), 2);
            EventManager.current.ShapeHit();
            EventManager.current.AddedScore(other.transform, _scoreData.colorShapePoints);
            EventManager.current.PlayerGetScore(_scoreData.colorShapePoints);
        }

        if (other.CompareTag("Blue Shape"))
        {
            EventManager.current.DamageHit();
            EventManager.current.WrongShapeHit();
            StartCoroutine(HitFeedback());
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Parallel Red Shape"))
        {
            int random = Random.Range(0, 3);
            SoundManager.Instance.PlaySound(_clip[random]);
            Destroy(Instantiate(redExplostion, other.transform.position, Quaternion.identity), 2);
            Destroy(other.gameObject);
            EventManager.current.ShapeHit();
            EventManager.current.PlayerGetScore(_scoreData.colorShapePoints);
        }
    }

    IEnumerator HitFeedback() {

        Color startColor = transform.GetComponent<MeshRenderer>().materials[0].color;

        transform.GetComponent<MeshRenderer>().materials[0].color = Color.white;

        yield return new WaitForSeconds(0.1f);

        transform.GetComponent<MeshRenderer>().materials[0].color = startColor;

        yield return null;
    }
    

}
