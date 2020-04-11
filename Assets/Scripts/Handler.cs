using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        //audioData.PlayScheduled(AudioSettings.dspTime + 2000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
