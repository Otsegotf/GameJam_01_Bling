using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : Singleton<MusicPlayer>
{
    public AudioClip[] MusicTracks;

    public AudioClip _currentPlayedClip;

    public AudioClip SirenClip;

    public AudioClip FailClip;

    public AudioClip SuspenceClip;

    public AudioSource Source;
    public void Play()
    {
        if (Source.isPlaying)
            return;
        Source.clip = MusicTracks[Random.Range(0, MusicTracks.Length)];
        Source.Play();
    }

    public void PlayFail()
    {
        Source.clip = FailClip;
        Source.Play();
    }

    public void PlaySuspence()
    {
        Source.clip = SuspenceClip;
        Source.Play();
    }
    public void PlaySiren()
    {
        Source.clip = SirenClip;
        Source.Play();
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
