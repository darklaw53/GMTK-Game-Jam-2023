using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompSoundPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayStompSound()
    {
        GetComponent<AudioSource>().Play();
    }
}
