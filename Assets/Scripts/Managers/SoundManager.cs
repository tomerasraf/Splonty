using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectSource;

    [SerializeField] float chocker;

    bool _isPlaying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectSource.clip = clip;
        _effectSource.Play();
    }

    public void PlayOneShotSound(AudioClip clip)
    {
        StartCoroutine(SoundChocke(clip));
    }

    IEnumerator SoundChocke(AudioClip clip) {

        if (!_isPlaying) {
            _effectSource.PlayOneShot(clip);
        }

        _isPlaying = true;

        yield return new WaitForSeconds(chocker);

        _isPlaying = false;

        yield return null;
    }
}
