using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CorridoioManager : MonoBehaviour
{
    public bool firstObjectFound = false;
    public bool secondObjectFound = false;
    public bool doorOpen = false;
    public GameObject firstPersonController;
    public DoorOpener doorOpener;
    public TriggerFlashbackObject triggerFlashbackObject;
    public static CorridoioManager instance { get; private set; }
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

    private void Start()
    {
        firstPersonController = GameObject.FindGameObjectWithTag("Player");
        doorOpener = firstPersonController.GetComponent<DoorOpener>();
        triggerFlashbackObject = firstPersonController.GetComponent<TriggerFlashbackObject>();
        doorOpener.enabled = false;
        triggerFlashbackObject.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstObjectFound)
        {
            Debug.Log("First object found!");
            
            MySceneManager.instance.LoadNextScene("FlashbackInfermieria", LoadSceneMode.Single, () =>
            {
                Debug.Log("FlashbackInfermieria caricato con successo!");
            });
            Destroy(gameObject);
            Destroy(firstPersonController);
            
        }
        if (secondObjectFound)
        {
            Debug.Log("Second object found!");
            MySceneManager.instance.LoadNextScene("FlashbackCelle", LoadSceneMode.Single, () => 
                {Debug.Log("FlashbackCelle caricato con successo!");});
            
            Destroy(gameObject);
        } 
        if (doorOpen)
        {
            Debug.Log("Door is now open!");
            MySceneManager.instance.LoadNextScene("ScenaFinaleElettroshock", LoadSceneMode.Single,
                () => { Debug.Log("ScenaFinaleElettrosh");
                });
            Destroy(gameObject);
        }
    }
}
