using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    
    public AudioClip moneyClip;
    public AudioClip bangClip;
    public AudioClip ouchClip;
    public AudioClip deathClip;
    public AudioClip rewindClip;
    public AudioClip[] jumpClips;

    bool rewindMusicIsPlaying = false;

    AudioSource audioSource;
    PlayerRewind playerRewind;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerRewind = FindObjectOfType<PlayerRewind>();

        if (!moneyClip || !bangClip ||!ouchClip ||!deathClip)
        {
            Debug.LogWarning("Sound manager error: an audioclip is missing");
        }
        if (!playerRewind)
        {
            Debug.LogWarning("Player rewind hasn't been found");
        }
    }

    private void Update()
    {
        Rewind();
    }

    public void PlayClip(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }

    public AudioClip randomJumpClip()
    {
        int numberOfClips = jumpClips.Length;

        if(numberOfClips < 1)
        {
            Debug.LogWarning("Unsufficient number of clips");
            return null;
        }

        int randomNumber = Random.Range(0, numberOfClips);
        AudioClip randomClip = jumpClips[randomNumber];
        return randomClip;
    }

    public void Rewind()
    {
        if(playerRewind.timeIsRewinding && !rewindMusicIsPlaying)
        {
            audioSource.clip = rewindClip;
            audioSource.volume = 0.05f;
            audioSource.Play();
            rewindMusicIsPlaying = true;
        }
        if(rewindMusicIsPlaying && !playerRewind.timeIsRewinding)
        {
            audioSource.Stop();
            rewindMusicIsPlaying = false;
        }
        
    }

}
