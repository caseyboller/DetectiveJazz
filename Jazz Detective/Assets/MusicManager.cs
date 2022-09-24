using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] songs;
    public AudioClip currentSong;
    public int index = 0;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentSong = songs[0];
        audioSource.clip = currentSong;
        PlayFirst();
    }

    private void PlayFirst()
    {
        Debug.Log("Playing " + currentSong.name);
        audioSource.Play();
        Invoke(nameof(PlayMusic), currentSong.length + 0.5f);
    }

    private void PlayMusic()
    {
        audioSource.Stop();
        currentSong = songs[UnityEngine.Random.Range(0, 3)];
        Debug.Log("Playing " + currentSong.name);
        audioSource.clip = currentSong;
        audioSource.Play();
        Invoke(nameof(PlayMusic), currentSong.length + 0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


}
