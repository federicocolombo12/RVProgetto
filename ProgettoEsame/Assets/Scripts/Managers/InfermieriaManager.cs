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
    [SerializeField] TransitionScript transitionManager;
   
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
        TransitionScript.instance.FadeIn(1);
    }

    // Update is called once per frame
    
    public void LoadCorridoio()
    {

       StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        TransitionScript.instance.FadeOut(1);
        yield return new WaitForSeconds(3);

        MySceneManager.instance.LoadNextScene("Corridoio1", LoadSceneMode.Single, () =>
        {
            // Trova il punto di spawn nella nuova scena
            TransitionScript.instance.FadeIn(1);
            GameObject spawnPoint = GameObject.Find("PlayerSpawnPos");
            if (spawnPoint != null)
            {
                Instantiate(firstPersonController, spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
            else
            {
                Debug.LogError("PlayerSpawnPos1 non trovato nella scena Corridoio1!");
            }
            GameObject colliderTag1 = GameObject.Find("ColliderInfermieria");
            
            colliderTag1.tag = "Untagged";
            Destroy(gameObject);
           
        });
    }

}
