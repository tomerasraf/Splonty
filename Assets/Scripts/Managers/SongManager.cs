using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [Header("Sound Source")]
    [SerializeField] AudioSource bpmSound;
    [SerializeField] AudioClip bpmSoundClip;

    [Header("Song Vars")]
    [SerializeField] float bpm;
    [SerializeField] int beatByMeter;
    private float beatLengthPerSecond;

    [Header("Script References")]
    [SerializeField] GameManager _gameManager;

    private void OnEnable()
    {
        EventManager.current.onStartGameTouch += Fart;
    }

    private void Start()
    {
        beatLengthPerSecond = 60 / bpm;
        _gameManager.levelSpeed = beatByMeter / beatLengthPerSecond;
    }

  

    private void Fart() {
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound() {
        yield return new WaitForSeconds(0.2f);

        while (true)
        {
            yield return new WaitForSeconds(beatLengthPerSecond);

            bpmSound.clip = bpmSoundClip;
            bpmSound.Play();
        }
    }
}
