using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public Canvas pauseCanvas; // Riferimento al Canvas del menu di pausa
    private FirstPersonController firstPersonController; // Riferimento al componente che gestisce il movimento della camera
    public static PauseMenuManager instance { get; private set; }
    public PauseSoundManager pauseSoundManager; // Riferimento al nuovo script
    public bool abilitaSuonoMenu = true; // Booleano per attivare/disattivare il suono

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (pauseCanvas.enabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Start()
    {
        // Nascondi il menu di pausa all'avvio
        pauseCanvas.enabled = false;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            pauseCanvas.enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) // Tasto per mettere in pausa
        {
            if (pauseCanvas.enabled)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        // Trova il First Person Controller nella scena corrente
        firstPersonController = FindObjectOfType<FirstPersonController>();

        // Mostra il menu di pausa
        Debug.Log("Menu attivato");
        pauseCanvas.enabled = true;
        Time.timeScale = 0f; // Ferma il tempo

        // Sblocca il cursore e rendilo visibile
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disabilita il movimento della camera del First Person Controller
        if (firstPersonController != null)
        {
            firstPersonController.enabled = false;
        }

        // Metti in pausa tutti gli audio
        AudioListener.pause = true;

        if (pauseSoundManager != null && abilitaSuonoMenu)
        {
            pauseSoundManager.RiproduciSuoni(); // Riproduce entrambi i suoni contemporaneamente
        }
    }

    public void ResumeGame()
    {
        // Nascondi il menu di pausa
        pauseCanvas.enabled = false;
        Time.timeScale = 1f; // Riprendi il tempo

        // Blocca il cursore e nascondilo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Riabilita il movimento della camera del First Person Controller
        if (firstPersonController != null)
        {
            firstPersonController.enabled = true;
        }

        // Riprendi tutti gli audio
        AudioListener.pause = false;

        // Ferma entrambi i suoni quando il gioco riprende
        if (pauseSoundManager != null)
        {
            pauseSoundManager.FermaSuoni(); // Ferma entrambi i suoni
        }
    }

    public void OpenOptions()
    {
        // Gestisci l'apertura delle opzioni (attualmente non fa nulla)
    }

    public void LoadMainMenu()
    {
        MySceneManager.instance.ResetSystem();
    }
}
