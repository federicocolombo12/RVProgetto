using UnityEngine;

public class CellGuardAudioManager : MonoBehaviour
{
    [SerializeField] private CellGuardNpc script; // Riferimento allo script NPC
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip talkingClip0;
    [SerializeField] private AudioClip talkingClip1;
    [SerializeField] private AudioClip talkingClip2;
    [SerializeField] private AudioClip talkingClip3;
    [SerializeField] private AudioClip talkingClip4;
    [SerializeField] private AudioClip talkingClip5;
    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip walkingClip;
    [SerializeField] private AudioClip talkingClip;
    bool notStarted = false;

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

 

    public  void PlayAudioClip(AudioClip clip)
    {
        if (audioSource.isPlaying && audioSource.clip == clip) return; // Evita di interrompere lo stesso audio
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip);
    }

    private void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
    public void TalkingClip0()
    {
        PlayAudioClip(talkingClip0);
    }
    public void TalkingClip1() { 
        PlayAudioClip(talkingClip1);
    }
    public void TalkingClip2()
    {
        PlayAudioClip(talkingClip2);
    }
    public void TalkingClip3()
    {
        PlayAudioClip(talkingClip3);
    }
    public void TalkingClip4()
    {
        PlayAudioClip(talkingClip4);
    }
    public void TalkingClip5()
    {
        PlayAudioClip(talkingClip0);
    }
    public void IdleClip() { 
        PlayAudioClip(idleClip);
    }
    public void WalkingClip() { 
        PlayAudioClip(walkingClip);
    }
    public void TalkingClip() { 
        PlayAudioClip(talkingClip);
    }
}