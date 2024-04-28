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
                audioSource.clip = sFXAudioClip[i];
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                else
                {
                    if (sFXAudioClip[i].name != "WinSFX")
                    {
                        audioSource.Stop();
                        audioSource.Play();
                    }
                }
            }
        }
    }
}