using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobAudioManager : MonoBehaviour
{
    public AudioClip[] StealingSounds;

    public AudioClip StealingTimeSound;

    public AudioClip _currentPlayedClip;

    public AudioSource Source;
    public void StartStealing()
    {
        if (Source.isPlaying)
            Stop();
        Source.clip = StealingSounds[Random.Range(0, StealingSounds.Length)];
        Source.Play();
    }

    public void SendStealWarning()
    {
        Source.PlayOneShot(StealingTimeSound);
    }

    public void Pause()
    {
        Source.Pause();
    }

    public void Resume()
    {
        Source.Play();
    }

    public void Stop()
    {
        Source.Stop();
    }
}
