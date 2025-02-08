using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLock : MonoBehaviour
{
    public GameObject queueManager;

    [SerializeField] private bool locked;
    public bool reachedPoint;
    FirstPersonController player;
    private bool positionSet;
    [SerializeField] private Transform targetPosition;
    
    [SerializeField] TriggerController triggerController;

    void Start()
    {
        locked = !QueueManager.instance.lineFinished;
        player = GetComponent<FirstPersonController>();
        triggerController = GetComponent<TriggerController>();
    }

    void Update()
    {
        locked = !QueueManager.instance.lineFinished;
        if (locked)
        {
            player.playerCanMove = false;
            if (player.gameObject.transform.position != targetPosition.position && !positionSet)
            {
                player.gameObject.transform.position = targetPosition.position;
                positionSet=true;
            }
            
        }
        else if (!locked && !reachedPoint)
        {
            player.playerCanMove = true;
            player.cameraCanMove = true;
            triggerController.enabled = true;
        }
        else if (!locked && reachedPoint)
        {
            player.playerCanMove = false;
            player.cameraCanMove = true;
            
        }
    }
}
