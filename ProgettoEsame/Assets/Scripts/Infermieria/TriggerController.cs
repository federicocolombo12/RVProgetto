using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PlayerLock player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.reachedPoint = true;
        }
    }
}
