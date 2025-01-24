using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CellaManager : MonoBehaviour
{
    public static CellaManager instance { get; private set; }
    public GameObject firstPersonController;
    public GameObject firstPersonControllerCorridoio1;

    [SerializeField] public bool coltelloNascosto = false;
    [SerializeField] public bool coltelloPreso = false;
    [SerializeField] public bool attivaGuardRoutine = false;
    [SerializeField] public bool attivaPazienteRoutine = false;

   

    private CellGuardNpc cellGuardNpc;
    private NpcScript npcScript;
    private Paziente0Script paziente0Script;

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
       
        // Trova i componenti nella scena
        cellGuardNpc = FindObjectOfType<CellGuardNpc>();
        npcScript = FindObjectOfType<NpcScript>();
        paziente0Script = FindObjectOfType<Paziente0Script>();

        if (cellGuardNpc == null)
        {
            Debug.LogError("CellGuardNpc non trovato nella scena!");
            return;
        }

        if (paziente0Script == null)
        {
            Debug.LogError("Paziente0Script non trovato nella scena!");
            return;
        }

        // Registra l'evento di fine animazione
        cellGuardNpc.OnAnimationEnd += HandleAnimationEnd;

        // Inizia le routine di comportamento dei personaggi
        StartCoroutine(StartCharacterRoutines());
    }

    private IEnumerator StartCharacterRoutines()
    {
        if (coltelloPreso)
        {
            attivaGuardRoutine = true;
        }

        if (attivaPazienteRoutine)
        {
           
            StartCoroutine(paziente0Script.PazienteRoutine());
        }

        yield return null;
    }

    private void HandleAnimationEnd()
    {
        // Quando l'animazione del guardiano termina, carica la nuova scena
        LoadCorridoio1Scene();
    }

    private void LoadCorridoio1Scene()
    {
        MySceneManager.instance.LoadNextScene("Corridoio1", LoadSceneMode.Single, () =>
        {
            // Trova il punto di spawn nella nuova scena
            GameObject spawnPoint = GameObject.Find("PlayerSpawnPos1");
            if (spawnPoint != null)
            {
                Instantiate(firstPersonControllerCorridoio1, spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
            else
            {
                Debug.LogError("PlayerSpawnPos1 non trovato nella scena Corridoio1!");
            }
            Destroy(gameObject);
        });
    }
}
