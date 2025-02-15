using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager instance { get; private set; }
    private AsyncOperation asyncLoadOperation;

    public string currentSceneName; // Nome della scena attuale
    public string nextSceneName; // Nome della prossima scena
    public bool isInFlashback; // Indica se siamo in un flashback

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
    public void ResetSystem()
    {
        // Trova tutti gli oggetti marcati con DontDestroyOnLoad e distruggili
        GameObject[] dontDestroyObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in dontDestroyObjects)
        {
            if (obj.scene.buildIndex == -1) // Gli oggetti con -1 sono in DontDestroyOnLoad
            {
                if (obj.GetComponent<EventSystem>() == null)
                {
                    Destroy(obj);
                }

                
            }
        }
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Ricarica la scena iniziale
        SceneManager.LoadScene(0); // Assumendo che la scena iniziale sia la 0 nell'ordine di build
    }

}
