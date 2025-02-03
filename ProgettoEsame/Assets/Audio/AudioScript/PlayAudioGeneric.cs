using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioGeneric : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayAudio()
    {
        audioSource.Play();
    }
}
