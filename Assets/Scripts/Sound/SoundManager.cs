using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] sFXAudioClip;

    private AudioSource audioSource;

    public static SoundManager soundManager;

    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

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
                else // si el efecto de sonido está en reproducción lo detiene y lo repite
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
                audioSource.clip = sFXAudioClip[i]; // se asigna para poder compararlo en un nuevo llamado
            }
        }
    }
}
