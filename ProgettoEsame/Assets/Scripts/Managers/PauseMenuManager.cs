using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public Canvas pauseCanvas; // Riferimento al Canvas del menu di pausa
    public MonoBehaviour firstPersonController; // Riferimento al componente che gestisce il movimento della camera
    public PauseMenuManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        // Nascondi il menu di pausa all'avvio
        pauseCanvas.enabled = false;
    }

    void Update()
    {
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
    }

    public void OpenOptions()
    {
        // Gestisci l'apertura delle opzioni (attualmente non fa nulla)
    }

    public void LoadMainMenu()
    {
        // Carica la scena del menu principale
        Time.timeScale = 1f; // Assicurati che il tempo riprenda
        SceneManager.LoadScene("TitleScreen");
    }
}

