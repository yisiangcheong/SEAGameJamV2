using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource myAudioSourceSFX;
    public AudioClip messageOnTapTone;
    public AudioClip messageOnSpawnTone;
    public AudioClip postTone;

    public void playPostTone()
    {
        myAudioSourceSFX.PlayOneShot(postTone, 0.4f);
    }

    public void playMessageOnTapTone()
    {
        myAudioSourceSFX.PlayOneShot(messageOnTapTone, 2.0f);
    }

    public void playMessageOnSpawnTone()
    {
        myAudioSourceSFX.PlayOneShot(messageOnSpawnTone, 2.0f);
    }
}
