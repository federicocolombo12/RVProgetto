using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager instance { get; private set; }
    [SerializeField] private Slider volumeSlider; // Assegna lo slider nell'Inspector

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("GameVolume", 1f);
            AudioListener.volume = volumeSlider.value;

            // Aggiunge l'evento per rilevare cambiamenti nello slider
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
        // Imposta il valore dello slider al volume salvato (o 1 se non esiste)
        
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("GameVolume", volume); // Salva il volume tra le sessioni
        PlayerPrefs.Save();
    }
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
