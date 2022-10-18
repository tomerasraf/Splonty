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
        bpmSound.clip = bpmSoundClip;
        beatLengthPerSecond = 60 / bpm;
        _gameManager.levelSpeed = beatByMeter / beatLengthPerSecond;
    }

  

    private void Fart() {
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound() {

        while (true)
        {
            bpmSound.Play();
            yield return new WaitForSeconds(beatLengthPerSecond);
        }
    }
}
