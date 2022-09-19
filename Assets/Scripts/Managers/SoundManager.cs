using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectSource;

    [Header("Clips")]
    [SerializeField] AudioClip ambienceStart;

    [Header("Chockers")]
    [SerializeField] float chocker;
    [SerializeField] float missChocker;

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

    private void Start()
    {
        playMusic(ambienceStart);
    }

    public void playMusic(AudioClip musicClip)
    {
        _musicSource.clip = musicClip;
        _musicSource.Play();
    }

    public void StopPlayingSound()
    {
        _effectSource.Stop();
        _musicSource.Stop();
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

    public void PlayMissChocker(AudioClip clip)
    {
        StartCoroutine(MissChocke(clip));
    }

    IEnumerator SoundChocke(AudioClip clip)
    {

        if (!_isPlaying)
        {
            _effectSource.PlayOneShot(clip);
        }

        _isPlaying = true;

        yield return new WaitForSeconds(chocker);

        _isPlaying = false;

        yield return null;
    }

    IEnumerator MissChocke(AudioClip clip)
    {

        if (!_isPlaying)
        {
            _effectSource.PlayOneShot(clip);
        }

        _isPlaying = true;

        yield return new WaitForSeconds(missChocker);

        _isPlaying = false;

        yield return null;
    }
}
