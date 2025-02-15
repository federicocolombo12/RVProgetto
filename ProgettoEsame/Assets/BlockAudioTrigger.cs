using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAudioTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!audioSource.isPlaying)
            {

            audioSource.Play(); }
            
        }
    }
}
