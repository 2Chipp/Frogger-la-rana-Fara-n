using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] sFXAudioClip;

    private AudioSource audioSource;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySFX(string name)
    {
        for (int i = 0; i < sFXAudioClip.Length; i++)
        {
            if (sFXAudioClip[i].name == name)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(sFXAudioClip[i]);
                }
                else
                {
                    if (sFXAudioClip[i] != audioSource.clip)
                    {
                        audioSource.PlayOneShot(sFXAudioClip[i]);
                    }
                    else
                    {
                        audioSource.Stop();
                        audioSource.PlayOneShot(sFXAudioClip[i]);
                    }
                }
                audioSource.clip = sFXAudioClip[i];
            }
        }
    }
}
