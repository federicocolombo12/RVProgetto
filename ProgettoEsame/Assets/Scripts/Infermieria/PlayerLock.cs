using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLock : MonoBehaviour
{
    public GameObject queueManager;
    
    [SerializeField] private bool locked;
    FirstPersonController player;
    void Start()
    {
        
        locked = !QueueManager.instance.lineFinished;
        player = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        locked = !QueueManager.instance.lineFinished;
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
