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
    public bool startAnimation = false;
    [SerializeField] private Torcia torciaScript;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (torchFound)
        {

            doorOpenable = true;
            

        }
        if (doorOpenable)
        {
            torciaScript.enabled = true;

        }
        if (doorOpen)
        {

            MySceneManager.instance.LoadNextScene("Corridoio1", LoadSceneMode.Additive, () =>
            {
                startAnimation=true;
            });
            
            Destroy(gameObject);
            
        }
    }
    
}
