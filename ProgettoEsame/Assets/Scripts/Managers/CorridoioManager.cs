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
    public bool startAnimationPorta = false;
    [SerializeField] private float walkSpeed;
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

    

    // Update is called once per frame
    void Update()
    {
        if (firstPersonController == null)
        {
            firstPersonController = GameObject.FindGameObjectWithTag("Player");
            doorOpener = firstPersonController.GetComponent<DoorOpener>();
            triggerFlashbackObject = firstPersonController.GetComponent<TriggerFlashbackObject>();
            doorOpener.enabled = false;
            triggerFlashbackObject.enabled = true;
            firstPersonController.GetComponent<FirstPersonController>().fov = 85;
            firstPersonController.GetComponent<FirstPersonController>().walkSpeed = walkSpeed;
        }
        if (firstObjectFound)
        {

            StartCoroutine(LoadInfermieria());
        }
        if (secondObjectFound)
        {
            StartCoroutine(LoadCelle());
        } 
        if (doorOpen)
        {
            StartCoroutine(LoadFinale());
        }
    }
    public void FlickerTorch()
    {
        firstPersonController.GetComponent<Torcia>().Flickering();
    }
    IEnumerator LoadInfermieria()
    {
        Debug.Log("First object found!");
        TransitionScript.instance.FadeOut();
        yield return new WaitForSeconds(2f);
            
        MySceneManager.instance.LoadNextScene("FlashbackInfermieria", LoadSceneMode.Single, () =>
        {
            Debug.Log("FlashbackInfermieria caricato con successo!");
        });
        Destroy(gameObject);
        Destroy(firstPersonController);
    }
    IEnumerator LoadCelle()
    {
        TransitionScript.instance.FadeOut();
        yield return new WaitForSeconds(2f);
            
        Debug.Log("Second object found!");
        MySceneManager.instance.LoadNextScene("FlashbackCelle", LoadSceneMode.Single, () => 
            {Debug.Log("FlashbackCelle caricato con successo!");});
            
        Destroy(gameObject);
        Destroy(firstPersonController);
    }
    IEnumerator LoadFinale()
    {
        
        TransitionScript.instance.FadeOut();
        yield return new WaitForSeconds(2f);
            
        Debug.Log("Door is now open!");
        MySceneManager.instance.LoadNextScene("ScenaFinaleElettroshock", LoadSceneMode.Additive,
            () => {
                startAnimationPorta = true;
                TransitionScript.instance.FadeIn();
            });
        Destroy(gameObject);
    }
}
