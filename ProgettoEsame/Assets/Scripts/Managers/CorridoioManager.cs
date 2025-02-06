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

            doorOpener.enabled = false;

            firstPersonController.GetComponent<FirstPersonController>().fov = 85;
            firstPersonController.GetComponent<FirstPersonController>().walkSpeed = walkSpeed;
        }

    }
    public void FlickerTorch()
    {
        firstPersonController.GetComponent<Torcia>().Flickering();
    }
    public void LoadInfermieria()
    {
        StartCoroutine(LoadInfermieriaCoroutine());
    }
    IEnumerator LoadInfermieriaCoroutine()
    {
        Debug.Log("First object found!");
        TransitionScript.instance.FadeOut(1);
        yield return new WaitForSeconds(1);

        MySceneManager.instance.LoadNextScene("FlashbackInfermieria", LoadSceneMode.Single, () =>
        {
            TransitionScript.instance.FadeIn(1);

        });

        Destroy(firstPersonController);
        Destroy(gameObject);
    }
    public void LoadCelle()
    {
        StartCoroutine(LoadCelleCoroutine());
    }
    IEnumerator LoadCelleCoroutine()
    {
        TransitionScript.instance.FadeOut(1);
        yield return new WaitForSeconds(1);

        Debug.Log("Second object found!");
        MySceneManager.instance.LoadNextScene("FlashbackCelle", LoadSceneMode.Single, () =>
        {
            Debug.Log("FlashbackCelle caricato con successo!");
            TransitionScript.instance.FadeIn(1);

        });
        Destroy(firstPersonController);
        Destroy(gameObject);


    }
    public void LoadFinale()
    {
        StartCoroutine(LoadFinaleCoroutine());
    }
    IEnumerator LoadFinaleCoroutine()
    {

        TransitionScript.instance.FadeOut(0);
        yield return new WaitForSeconds(1);

        Debug.Log("Door is now open!");
        MySceneManager.instance.LoadNextScene("ScenaFinaleElettroshock", LoadSceneMode.Additive,
            () => {
                startAnimationPorta = true;
                TransitionScript.instance.FadeIn(0);

            });
        Destroy(gameObject);
    }
    public void LoadPassato()
    {
        StartCoroutine(LoadPassatoCoroutine());
    }
    IEnumerator LoadPassatoCoroutine()
    {
        TransitionScript.instance.FadeOut(1);
        yield return new WaitForSeconds(1);

        Debug.Log("Door is now open!");
        MySceneManager.instance.LoadNextScene("ScenaFinaleElettroshockPassato", LoadSceneMode.Single,
            () => {

                TransitionScript.instance.FadeIn(1);

            });
        Destroy(gameObject);
    }
}