using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Paziente0Script script;
    [SerializeField] AudioSource audioSource;
    void Start()
    {
        script = GetComponent<Paziente0Script>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (script.talking)
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
