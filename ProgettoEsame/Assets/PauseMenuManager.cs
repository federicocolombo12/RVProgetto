using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public Canvas pauseCanvas; // Riferimento al Canvas del menu di pausa
    public RawImage backgroundImage; // UI RawImage per mostrare lo screenshot
    public string firstPersonCameraName = "PlayerCamera"; // Nome della camera del First Person Controller
    public string pauseMenuCameraName = "PauseMenuCamera"; // Nome della camera del menu di pausa
    public Button resumeButton; // Bottone Resume
    public Button optionsButton; // Bottone Options
    public Button mainMenuButton; // Bottone Main Menu

    private Camera firstPersonCamera;
    private Camera pauseMenuCamera;
    private Texture2D screenshotTexture;

    void Start()
    {
        // Trova le camere per nome
        firstPersonCamera = GameObject.Find(firstPersonCameraName).GetComponent<Camera>();
        pauseMenuCamera = GameObject.Find(pauseMenuCameraName).GetComponent<Camera>();

        // Nascondi il menu di pausa e disabilita la camera del menu di pausa all'avvio
        pauseCanvas.enabled = false;
        pauseMenuCamera.enabled = false;
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
        // Cattura uno screenshot del gioco
        screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        // Mostra lo screenshot come sfondo
        backgroundImage.texture = screenshotTexture;
        backgroundImage.enabled = true;

        // Mostra il menu di pausa
        pauseCanvas.enabled = true;
        Time.timeScale = 0f; // Ferma il tempo

        // Disabilita la camera del First Person Controller e abilita quella del menu di pausa
        firstPersonCamera.enabled = false;
        pauseMenuCamera.enabled = true;
    }

    public void ResumeGame()
    {
        // Nascondi il menu di pausa
        pauseCanvas.enabled = false;
        Time.timeScale = 1f; // Riprendi il tempo

        // Rimuovi lo screenshot
        backgroundImage.texture = null;
        backgroundImage.enabled = false;

        // Abilita la camera del First Person Controller e disabilita quella del menu di pausa
        firstPersonCamera.enabled = true;
        pauseMenuCamera.enabled = false;
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
