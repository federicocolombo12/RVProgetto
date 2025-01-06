using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneManager : MonoBehaviour
{
    public static FirstSceneManager instance { get; private set; }
    [SerializeField] bool torchFound = false;
    public bool doorOpenable = false;
    public bool doorOpen = false;
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
        currentScene=UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        currentSceneName = currentScene.name;
        
        SceneManager.instance.nextSceneName = "ScenaIniziale";
    }

    // Update is called once per frame
    void Update()
    {
        if (torchFound)
        {
            Debug.Log("Torch found!");
            doorOpenable = true;
        }
        if (doorOpenable)
        {
            Debug.Log("Door is now openable!");
            
        }
        if (doorOpen)
        {
            Debug.Log("Door is now open!");
            StartCoroutine(SceneManager.instance.LoadNextScene("Corridoio1", LoadSceneMode.Additive));
            
            Destroy(gameObject);
            
        }
    }
    
}
