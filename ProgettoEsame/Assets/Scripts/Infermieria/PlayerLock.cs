using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLock : MonoBehaviour
{
    public GameObject queueManager;
    
    [SerializeField] private bool locked;
    public bool reachedPoint;
    FirstPersonController player;
    [SerializeField] InfermieraScript nurse;

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
        else if (!locked && !reachedPoint)
        {
            player.playerCanMove = true;
            player.cameraCanMove = false;
        }
        else if (!locked && reachedPoint)
        {
            player.playerCanMove = false;
            player.walkSpeed = 0;
            player.cameraCanMove = true;
            nurse.TriggerNurseTalk();

        }
    }
}
