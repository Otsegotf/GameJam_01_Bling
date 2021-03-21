using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JayAudioManager : Singleton<JayAudioManager>
{
    public AudioClip PickupSound;

    public AudioClip DropSound;

    public AudioClip GetCartSound;

    public AudioClip DropCartSound;

    public AudioClip _currentPlayedClip;

    public AudioSource Source;

    public void Pickup()
    {
        Source.PlayOneShot(PickupSound);
    }

    public void Drop()
    {
        Source.PlayOneShot(DropSound);
    }

    public void GetCart()
    {
        Source.PlayOneShot(GetCartSound);
    }
    public void DropCart()
    {
        Source.PlayOneShot(DropCartSound);
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
