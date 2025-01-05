using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLock : MonoBehaviour
{
    public GameObject queueManager;
    QueueManager queueManagerScript;
    [SerializeField] private bool locked;
    FirstPersonController player;
    void Start()
    {
        queueManagerScript = queueManager.GetComponent<QueueManager>();
        locked = !queueManagerScript.lineFinished;
        player = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        locked = !queueManagerScript.lineFinished;
        if (locked)
        {
            player.playerCanMove = false;
        }
        else
        {
            player.playerCanMove = true;
            player.cameraCanMove = false;
        }
    }
}
