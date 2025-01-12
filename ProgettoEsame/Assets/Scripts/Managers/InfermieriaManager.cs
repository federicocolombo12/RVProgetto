using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.SceneManagement;

public class InfermieriaManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static InfermieriaManager instance { get; private set; }
    public GameObject firstPersonController;
    public Transform playerSpawnPoint;
    public bool pastigliaTrovata = false;
    public bool isInRow = false;
    public bool medicinaPresa = false;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (medicinaPresa)
        {
            MySceneManager.instance.LoadNextScene("Corridoio1", LoadSceneMode.Single, () =>
            {
                Instantiate(firstPersonController, playerSpawnPoint.position, playerSpawnPoint.rotation);
                Destroy(gameObject);
            });
            
        }
    }
}
