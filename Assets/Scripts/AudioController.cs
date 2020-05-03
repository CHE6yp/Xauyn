using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public AudioClip loop;
    public AudioClip right;
    public AudioClip wrong;

    public AudioSource loopSource;
    public AudioSource soundSource;

    private void Awake()
    {
        instance = this;
    }

    public void Right()
    {
        soundSource.PlayOneShot(right);
    }

    public void Wrong()
    {
        soundSource.PlayOneShot(wrong);
    }
}
