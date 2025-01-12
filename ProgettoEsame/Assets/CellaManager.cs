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
    public bool oggettoNascosto = false;
    public bool coltelloPosato = false;

    private CellGuardNpc cellGuardNpc;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }

    void Start()
    {
        // Trova il componente CellGuardNpc nella scena
        cellGuardNpc = FindObjectOfType<CellGuardNpc>();

        if (cellGuardNpc == null)
        {
            Debug.LogError("CellGuardNpc non trovato nella scena!");
            return;
        }

        // Registra l'evento di fine animazione
        cellGuardNpc.OnAnimationEnd += HandleAnimationEnd;
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

    void Update()
    {
        if (coltelloPosato)
        {
            oggettoNascosto = true;
            cellGuardNpc.SetOggettoNascosto(true);
        }
    }
}

