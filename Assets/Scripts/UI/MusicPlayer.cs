using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : Singleton<MusicPlayer>
{
    public AudioClip[] MusicTracks;

    public AudioClip _currentPlayedClip;

    public AudioSource Source;
  public void Play()
    {
        if (Source.isPlaying)
            return;
        Source.clip = MusicTracks[Random.Range(0, MusicTracks.Length)];
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
