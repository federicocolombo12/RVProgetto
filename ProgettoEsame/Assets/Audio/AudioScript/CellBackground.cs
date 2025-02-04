using System.Collections;
using UnityEngine;

public class CellBackground : MonoBehaviour
{
    public static CellBackground instance { get; private set; }
    public AudioSource backgroundMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySuspenseMusic()
    {
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
            Debug.Log("?? Musica di suspense avviata!");
        }
    }

    public void StopMusic()
    {
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
            Debug.Log("?? Musica fermata!");
        }
    }
}
