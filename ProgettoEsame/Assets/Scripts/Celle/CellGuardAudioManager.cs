using UnityEngine;

public class CellGuardAudioManager : MonoBehaviour
{
    [SerializeField] private CellGuardNpc script; // Riferimento allo script NPC
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip talkingClip1;
    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip walkingClip;
    [SerializeField] private AudioClip talkingClip;

    void Start()
    {
        // Ottieni i riferimenti necessari
        script = GetComponent<CellGuardNpc>();
        audioSource = GetComponent<AudioSource>();

        if (script == null || audioSource == null)
        {
            Debug.LogError("CellGuardNpc o AudioSource non trovati!");
        }
    }

    void Update()
    {
        ManageAudioStates();
    }

    private void ManageAudioStates()
    {
        if (script.audioTalkingClip1 && audioSource.clip != talkingClip1)
        {
            PlayAudioClip(talkingClip1);
        }
        else if (script.audioIdle && audioSource.clip != idleClip)
        {
            PlayAudioClip(idleClip);
        }
        else if (script.audioWalking && audioSource.clip != walkingClip)
        {
            PlayAudioClip(walkingClip);
        }
        else if (script.audioTalking && audioSource.clip != talkingClip)
        {
            PlayAudioClip(talkingClip);
        }
        else if (!script.audioTalking && !script.audioIdle && !script.audioWalking && !script.audioTalking)
        {
            StopAudio();
        }
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (audioSource.isPlaying && audioSource.clip == clip) return; // Evita di interrompere lo stesso audio
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}