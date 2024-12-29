using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance { get; private set; }
    private AsyncOperation asyncLoadOperation;
    private LoadSceneMode _loadSceneMode; // Load the scene additively

    public string currentSceneName; // Nome della scena attuale
    public string nextSceneName; // Nome della prossima scena
    public bool isInFlashback; // Indica se siamo in un flashback
    public FlashbackType currentFlashback; // Tipo di flashback attuale

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
        }
        
    }

    void Start()
    {
        Scene firstScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName("ScenaIniziale");
        currentSceneName = firstScene.name;
        
        // Initialize variables if needed
        StartCoroutine(LoadNextScene("ScenaIniziale"));
    }


    public IEnumerator LoadNextScene(string sceneToLoadName)
    {
        Scene sceneToLoad = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneToLoadName);
        if (sceneToLoad.IsValid() && sceneToLoad.isLoaded)
            yield break;

        asyncLoadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneToLoadName, _loadSceneMode);
        while (!asyncLoadOperation.isDone)
        {
            yield return null;
        }
        asyncLoadOperation = null;
        Scene loadedScene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneToLoadName);
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(loadedScene);

    }
    // Carica una scena specifica
    
    public IEnumerator UnloadScene(Scene sceneToUnload)
    {
        while (asyncLoadOperation != null && !asyncLoadOperation.isDone)
        {
            yield return null;
        }

        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneToUnload);
    }
    // Gestisce la transizione verso un flashback
    public void TransitionToFlashback(FlashbackType type)
    {
        
        if (type == FlashbackType.Infermeria)
        {
            Debug.Log("Transitioning to Infermeria flashback");
        }
        else if (type == FlashbackType.Cella)
        {
            Debug.Log("Transitioning to Cella flashback");
        }
        else if (type == FlashbackType.Elettroshock)
        {
            Debug.Log("Transitioning to Elettroshock flashback");
        }
         // Load the flashback scene
    }

    // Gestisce il ritorno dal flashback
    public void ReturnFromFlashback(FlashbackType type)
    {
        
        if (type == FlashbackType.Infermeria)
        {
            Debug.Log("Returning from Infermeria flashback");
        }
        else if (type == FlashbackType.Cella)
        {
            Debug.Log("Returning from Cella flashback");
        }
        else if (type == FlashbackType.Elettroshock)
        {
            Debug.Log("Returning from Elettroshock flashback");
        }
        // Implementation for returning from a flashback
    }

    // Gestisce gli effetti di transizione
    public void HandleFlashbackTransitionEffects()
    {
        // Implementation for handling flashback transition effects
    }
}

// Assuming FlashbackType is an enum defined elsewhere in your code
public enum FlashbackType
{
    // Define flashback types here
    Infermeria,
    Cella,
    Elettroshock


}