using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Raccolta_Vedi_Oggetto raccolta_Vedi_Oggetto_script;
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        raccolta_Vedi_Oggetto_script = GetComponent<Raccolta_Vedi_Oggetto>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (raccolta_Vedi_Oggetto_script.playAudio)
        {
            if (!audioSource.isPlaying)
            {
                AudioManager.instance.PlayAudioEffect(audioSource);
            }

            
        }
        else
        {
            AudioManager.instance.StopAudioEffect(audioSource);
        }
    }
}
