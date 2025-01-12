using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StanzaFinaleManager : MonoBehaviour
{
    public static StanzaFinaleManager instance { get; private set; }

    public bool Video = false;
    public GameObject videoObject;
    public bool Oggetto1 = false;
    public bool Oggetto2 = false;
    public bool Oggetto3 = false;
    public bool Oggetto4 = false;

    [SerializeField] Scene currentScene;
    [SerializeField] string currentSceneName;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;

        // Assicurati che l'oggetto video sia disabilitato all'inizio
        if (videoObject != null)
        {
            videoObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Oggetto video non assegnato.");
        }
    }

    void Update()
    {
        Debug.Log($"Oggetto1: {Oggetto1}, Oggetto2: {Oggetto2}, Oggetto3: {Oggetto3}, Oggetto4: {Oggetto4}");

        if (Oggetto1 && Oggetto2 && Oggetto3 && Oggetto4)
        {
            Video = true;
            Debug.Log("Tutti gli oggetti sono stati interagiti. Video impostato su true.");
        }

        if (Video)
        {
            if (videoObject != null && !videoObject.activeSelf)
            {
                videoObject.SetActive(true);
                Debug.Log("Video avviato.");
            }
        }
    }
}
