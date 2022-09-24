using System.Collections;
using UnityEngine;
using DG.Tweening;

public class RedSaberCollision : MonoBehaviour
{

    [SerializeField] private AudioClip[] _clip;

    [Header("Data")]
    [SerializeField] ScoreData _scoreData;
    [SerializeField] GameData _gameData;

    [Header("Effects")]
    [SerializeField] GameObject redExplostion;

    [Header("Sounds")]
    [SerializeField] AudioClip missClip;
    [SerializeField] AudioClip comboBreaker;

    [Header("Saber")]
    [SerializeField] MeshRenderer saber;

    [Header("Slashed Shapes")]
    [SerializeField] GameObject redSlashedShape;
    [SerializeField] Transform slashedShapeParent;

    Color startColor;
    bool canFeedback = true;

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
            SlashBall(other);
        }

        if (other.CompareTag("Blue Shape"))
        {
            if (_gameData.comboHits > 20)
            {
                SoundManager.Instance.PlayOneShotSound(comboBreaker);
                EventManager.current.DamageHit();
                EventManager.current.WrongShapeHit();

                if (canFeedback) { 
                    HitFeedbackRed();
                }

                Destroy(other.gameObject);
            }
            else
            {
                SoundManager.Instance.PlayOneShotSound(missClip);
                EventManager.current.DamageHit();
                EventManager.current.WrongShapeHit();

                if (canFeedback)
                {
                    HitFeedbackRed();
                }

                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Parallel Red Shape"))
        {
            int random = Random.Range(0, 3);
            SoundManager.Instance.PlayOneShotSound(_clip[random]);
            Destroy(Instantiate(redExplostion, other.transform.position, Quaternion.identity), 2);
            Destroy(other.gameObject);
            EventManager.current.ShapeHit();
            EventManager.current.PlayerGetScore(_scoreData.colorShapePoints);
        }
    }

    void SlashBall(Collider other)
    {
        GameObject slashedBall = Instantiate(redSlashedShape, other.transform.position, Quaternion.identity, slashedShapeParent);
        Transform half1 = slashedBall.transform.GetChild(0);
        Transform half2 = slashedBall.transform.GetChild(1);

        half1.GetComponent<Rigidbody>().AddForce(half1.forward * 100, ForceMode.Impulse);
        half1.GetComponent<Rigidbody>().AddForce(half1.up * 25, ForceMode.Impulse);
        half1.GetComponent<Rigidbody>().AddForce(half1.right * 40, ForceMode.Impulse);
        half1.GetComponent<Rigidbody>().AddTorque(half1.right * 40, ForceMode.Impulse);

        half2.GetComponent<Rigidbody>().AddForce(half2.forward * 100, ForceMode.Impulse);
        half2.GetComponent<Rigidbody>().AddForce(half2.up * 25, ForceMode.Impulse);
        half2.GetComponent<Rigidbody>().AddForce(-half2.right * 40, ForceMode.Impulse);
        half2.GetComponent<Rigidbody>().AddTorque(-half1.right * 40, ForceMode.Impulse);

        Destroy(slashedBall, 3f);
    }

    void HitFeedbackRed()
    {
        canFeedback = false;
        startColor = saber.material.color;
        saber.material.DOColor(startColor, 0);
        saber.material.DOColor(Color.white, 0.2f).OnComplete(() =>
        {
            saber.material.DOColor(startColor, 0.2f).OnComplete(() => {

                canFeedback = true;
            });
        });
    }
}
