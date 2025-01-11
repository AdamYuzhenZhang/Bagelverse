using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioPlayer audio = GetComponent<AudioPlayer>();
        if (audio) { audio.Play(); }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
