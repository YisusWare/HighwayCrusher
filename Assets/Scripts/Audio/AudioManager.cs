using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource sfxSource;

    [SerializeField]
    AudioClip[] GameMusicTracks;
    [SerializeField]
    AudioClip[] radioMusicTracks;
    [SerializeField]
    AudioClip[] radioSpeechTracks;
    [SerializeField]
    AudioClip[] sfxClips;

    [SerializeField]
    AudioMixer audioMixer;

    public static AudioManager instance;

    private bool isPaused;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayRadio()
    {
        isPaused = false;
        StartCoroutine(PlayRadioCourutine());
    }

    private IEnumerator PlayRadioCourutine()
    {
        while (true)
        {
            int trackIndex = Random.Range(0, radioMusicTracks.Length);

            AudioClip nextClip = radioMusicTracks[trackIndex];

            musicSource.clip = nextClip;
            musicSource.Play();

            while (musicSource.isPlaying || isPaused)
            {
                yield return null; // Esperar hasta que termine o el juego se pause
            }
        }
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            isPaused = true;
        }
    }

    public void ResumeMusic()
    {
        if (isPaused)
        {
            musicSource.UnPause();
            isPaused = false;
        }
    }

    public void PlayTrack(int track)
    {
        musicSource.clip = GameMusicTracks[track];

        musicSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
    }

}
