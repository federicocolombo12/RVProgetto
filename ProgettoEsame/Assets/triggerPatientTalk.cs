using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerPatientTalk : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    [SerializeField] Animator animator;
    bool isTalking = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        PatientTalk();
    
    }
    public void PatientTalk()
    {
        if (audioSource.isPlaying && !isTalking)
        {
            animator.SetTrigger("Talking");
            isTalking = true;
        }
        else if (!audioSource.isPlaying)
        {
            
            isTalking = false;
        }
        
    }
}
