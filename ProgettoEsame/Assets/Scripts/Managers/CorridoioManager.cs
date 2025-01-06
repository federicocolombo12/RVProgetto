using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorridoioManager : MonoBehaviour
{
    public bool firstObjectFound = false;
    public bool secondObjectFound = false;
    public bool doorOpen = false;   
    

    // Update is called once per frame
    void Update()
    {
        if (firstObjectFound)
        {
            Debug.Log("First object found!");
            StartCoroutine(SceneManager.instance.UnloadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene()));
            StartCoroutine(SceneManager.instance.LoadNextScene("FlashbackInfermieria", LoadSceneMode.Single));
            Destroy(gameObject);
            
        }
        if (secondObjectFound)
        {
            Debug.Log("Second object found!");
            StartCoroutine(SceneManager.instance.LoadNextScene("FlashbackCelle", LoadSceneMode.Single));
            StartCoroutine(SceneManager.instance.UnloadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene()));
            Destroy(gameObject);
        } 
        if (doorOpen)
        {
            Debug.Log("Door is now open!");
            StartCoroutine(SceneManager.instance.LoadNextScene("ScenaFinaleElettroshock", LoadSceneMode.Single));
            StartCoroutine(SceneManager.instance.UnloadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene()));
            Destroy(gameObject);
        }
    }
}
