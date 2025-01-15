using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static PauseMenuManager Instance; // Istanza singleton

    public Canvas pauseCanvas; // Riferimento al Canvas del menu di pausa
    public RawImage backgroundImage; // UI RawImage per mostrare lo screenshot
    public Button resumeButton; // Bottone per riprendere il gioco
    public Button optionsButton; // Bottone per le opzioni (non fa nulla)
    public Button mainMenuButton; // Bottone per tornare al menu principale

    private Texture2D screenshotTexture;
    private string previousScene;

    void Awake()
    {
        // Implementa il Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Preserva l'oggetto quando si cambia scena
        }
        else
        {
            Destroy(gameObject); // Evita duplicati
        }
    }

    void Start()
    {
        // Assegna i metodi ai bottoni
        resumeButton.onClick.AddListener(ResumeGame);
        optionsButton.onClick.AddListener(OpenOptions);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Tasto per mettere in pausa
        {
            if (SceneManager.GetActiveScene().name != "PauseMenu")
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        // Memorizza la scena corrente
        previousScene = SceneManager.GetActiveScene().name;

        // Cattura uno screenshot del gioco
        screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        // Carica la scena del menu di pausa
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);

        // Mostra lo screenshot come sfondo
        backgroundImage.texture = screenshotTexture;
        backgroundImage.enabled = true;

        // Mostra il menu di pausa
        pauseCanvas.enabled = true;
        Time.timeScale = 0f; // Ferma il tempo
    }

    void ResumeGame()
    {
        // Nascondi il menu di pausa
        pauseCanvas.enabled = false;
        Time.timeScale = 1f; // Riprendi il tempo

        // Rimuovi lo screenshot
        backgroundImage.texture = null;
        backgroundImage.enabled = false;

        // Torna alla scena precedente
        SceneManager.UnloadSceneAsync("PauseMenu");
        SceneManager.LoadScene(previousScene);
    }

    void OpenOptions()
    {
        // Non fa nulla per ora
        Debug.Log("Opzioni aperte (non implementato)");
    }

    void LoadMainMenu()
    {
        // Riprendi il tempo prima di caricare la nuova scena
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Assicurati che la scena "MainMenu" esista
    }
}
