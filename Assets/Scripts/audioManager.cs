using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : Singleton<audioManager>
{
    public AudioSource audioPlayer;

    public AudioClip explosion, hit, jump, yeet;
    public AudioClip[] dialogues;

    public void ExplosionSFX()
    {
        audioPlayer.PlayOneShot(explosion, 1.0f);
    }

    public void hitSFX()
    {
        audioPlayer.PlayOneShot(hit, 1.0f);
    }

    public void jumpSFX()
    {
        audioPlayer.PlayOneShot(jump, 1.0f);
    }

    public void gotHit()
    {
        audioPlayer.PlayOneShot(yeet, 1.0f);
    }

    public void playDialogue()
    {
        audioPlayer.PlayOneShot(dialogues[Random.Range(0, dialogues.Length)], 1.0f);
    }
    
}
