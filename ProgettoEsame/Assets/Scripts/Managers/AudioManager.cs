using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void PlayAudioEffect(AudioSource audioSource)
    {


        audioSource.Play();

    }
    public void StopAudioEffect(AudioSource audioSource)
    {

        audioSource.Stop();
    }
}