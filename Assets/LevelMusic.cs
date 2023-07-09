using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    public AudioClip regularMusic, alertMusic;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playRegularMusic(){
        if (audioSource.clip != regularMusic){
            audioSource.clip = regularMusic;
            audioSource.volume = 1f;
            audioSource.Play();
        }
    }
    public void playAlertMusic(){
        if (audioSource.clip != alertMusic){
            audioSource.clip = alertMusic;
            audioSource.volume = 0.4f;
            audioSource.Play();
        }
    }
}
