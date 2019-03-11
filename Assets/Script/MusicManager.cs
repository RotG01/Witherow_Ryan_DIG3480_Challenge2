using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource BM;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void ChangeBM(AudioClip music)
    {
        if(BM.clip.name == music.name)
        {
            return;
        }

        BM.Stop();
        BM.clip = music;
        BM.Play();
    }
}
