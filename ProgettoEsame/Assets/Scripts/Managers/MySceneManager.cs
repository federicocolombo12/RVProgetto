using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance { get; private set; }
    private AsyncOperation asyncLoadOperation;

    public string currentSceneName; // Nome della scena attuale
    public string nextSceneName; // Nome della prossima scena
    public bool isInFlashback; // Indica se siamo in un flashback
    public FlashbackType currentFlashback;
    // Tipo di flashback attuale

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

    void Start()
    {
        
    }

    public void LoadNextScene(string sceneToLoadName, LoadSceneMode loadSceneMode, Action onSceneLoaded)
    {
        if (asyncLoadOperation != null)
        {
            Debug.LogWarning("Una scena è già in caricamento. Attendere il completamento.");
            return;
        }

        StartCoroutine(LoadSceneCoroutine(sceneToLoadName, loadSceneMode, onSceneLoaded));
    }

    private IEnumerator LoadSceneCoroutine(string sceneToLoadName, LoadSceneMode loadSceneMode, Action onSceneLoaded)
    {
        asyncLoadOperation = SceneManager.LoadSceneAsync(sceneToLoadName, loadSceneMode);

        while (!asyncLoadOperation.isDone)
        {
            yield return null;
        }

        asyncLoadOperation = null;

        Scene loadedScene = SceneManager.GetSceneByName(sceneToLoadName);
        if (loadedScene.IsValid())
        {
            SceneManager.SetActiveScene(loadedScene);
            currentSceneName = sceneToLoadName;

            onSceneLoaded?.Invoke(); // Chiama il callback al termine del caricamento
        }
        else
        {
            Debug.LogError($"Errore nel caricamento della scena {sceneToLoadName}");
        }
    }

    public void UnloadScene(string sceneToUnloadName, Action onSceneUnloaded)
    {
        Scene sceneToUnload = SceneManager.GetSceneByName(sceneToUnloadName);
        if (!sceneToUnload.IsValid())
        {
            Debug.LogError($"La scena {sceneToUnloadName} non è valida o non è caricata.");
            return;
        }

        StartCoroutine(UnloadSceneCoroutine(sceneToUnload, onSceneUnloaded));
    }

    private IEnumerator UnloadSceneCoroutine(Scene sceneToUnload, Action onSceneUnloaded)
    {
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneToUnload);

        while (!unloadOperation.isDone)
        {
            yield return null;
        }

        onSceneUnloaded?.Invoke(); // Chiama il callback al termine dello scaricamento
    }

    public void TransitionToFlashback(FlashbackType type)
    {
        string flashbackScene = type.ToString(); // Supponendo che il nome della scena corrisponda all'enum
        Debug.Log($"Transitioning to {flashbackScene} flashback");

        LoadNextScene(flashbackScene, LoadSceneMode.Additive, () =>
        {
            Debug.Log($"Flashback {flashbackScene} loaded.");
            isInFlashback = true;
            currentFlashback = type;
        });
    }

    public void ReturnFromFlashback(FlashbackType type)
    {
        string flashbackScene = type.ToString();
        Debug.Log($"Returning from {flashbackScene} flashback");

        UnloadScene(flashbackScene, () =>
        {
            Debug.Log($"Flashback {flashbackScene} unloaded.");
            isInFlashback = false;
            currentFlashback = default;
        });
    }
}

public enum FlashbackType
{
    Infermeria,
    Cella,
    Elettroshock
}
